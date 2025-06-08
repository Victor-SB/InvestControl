using Confluent.Kafka;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using InvestControl.Infrastructure.Context;
using InvestControl.Domain.Entities;
using InvestControl.Worker.Policies;
using Polly.Retry;
using Polly.CircuitBreaker;

namespace InvestControl.Worker

{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory, IConfiguration config)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _config = config;

            _retryPolicy = ResiliencePolicy.RetryPolicy;
            _circuitBreakerPolicy = ResiliencePolicy.CircuitBreaker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _config["Kafka:Broker"],
                GroupId = "cotacoes-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("nova-cotacao");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(stoppingToken);

                    _ = Task.Run(async () =>
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        try
                        {
                            var cotacao = JsonSerializer.Deserialize<Cotacao>(
                                result.Message.Value,
                                new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                });

                            if (cotacao != null)
                            {
                                _logger.LogInformation("Cotação recebida: ativoId={0}, preco={1}, data={2}", cotacao.AtivoId, cotacao.PrecoUnitario, cotacao.DataHora);

                                if (cotacao.AtivoId == 0 || cotacao.PrecoUnitario <= 0 || cotacao.DataHora == default)
                                {
                                    _logger.LogError("Cotação inválida detectada. Dados: ativoId={0}, preco={1}, data={2}",
                                        cotacao.AtivoId, cotacao.PrecoUnitario, cotacao.DataHora);
                                    return;
                                }

                                await _retryPolicy
                                      .WrapAsync(_circuitBreakerPolicy)
                                      .ExecuteAsync(async () =>
                                      {
                                          //Teste de Falha
                                          //if (cotacao.AtivoId == 999)
                                          //{
                                          //    throw new Exception("Simulação de falha forçada para testar Retry e Circuit Breaker");
                                          //}

                                          // Ajuste de Hora
                                          cotacao.PrecoUnitario = Math.Round(cotacao.PrecoUnitario, 1);
                                          cotacao.DataHora = cotacao.DataHora.AddTicks(-(cotacao.DataHora.Ticks % TimeSpan.TicksPerSecond));

                                          db.Cotacoes.Add(cotacao);

                                          // Atualizar P&L na posição correspondente
                                          var posicoes = db.Posicoes
                                              .Where(p => p.AtivoId == cotacao.AtivoId)
                                              .ToList();

                                          foreach (var p in posicoes)
                                          {
                                              p.PnL = (cotacao.PrecoUnitario - p.PrecoMedio) * p.Quantidade;
                                          }

                                          await db.SaveChangesAsync();
                                          _logger.LogInformation("Cotação salva e PnL atualizado para ativoId {0}", cotacao.AtivoId);
                                      });
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Erro ao processar cotação: {0}", ex.Message);
                        }
                    }, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Erro ao consumir Kafka: {0}", ex.Message);
                }

            }
        }
    }
}