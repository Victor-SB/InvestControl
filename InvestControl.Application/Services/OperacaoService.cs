using InvestControl.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Application.Services;

public class OperacaoService
{
    /// <summary>
    /// Calcula o preço médio ponderado com base em múltiplas compras.
    /// </summary>
    public decimal CalcularPrecoMedio(List<PrecoMedioDto> operacoes)
    {
        if (operacoes == null || operacoes.Count == 0)
            throw new ArgumentException("A lista de operações não pode ser nula ou vazia.");

        int totalQuantidade = 0;
        decimal somaPonderada = 0;

        foreach (var operacao in operacoes)
        {
            if (operacao.Quantidade <= 0 || operacao.PrecoUnitario <= 0)
                throw new ArgumentException("Valores inválidos de quantidade ou preço unitário.");

            totalQuantidade += operacao.Quantidade;
            somaPonderada += operacao.PrecoUnitario * operacao.Quantidade;
        }

        return totalQuantidade > 0 ? Math.Round(somaPonderada / totalQuantidade, 2) : 0;
    }

    /// <summary>
    ///  Calcula o P&L da posição atual com base na última cotação.
    /// </summary>
    public decimal CalcularPnL(decimal precoAtual, decimal precoMedio, int quantidade)
    {
        return Math.Round((precoAtual - precoMedio) * quantidade, 2);
    }
}
