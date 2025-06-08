using InvestControl.Application.DTOs;
using InvestControl.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Tests.Services;

public class OperacaoServiceTests
{
    private readonly OperacaoService _service;

    public OperacaoServiceTests()
    {
        _service = new OperacaoService();
    }

    [Fact]
    public void CalcularPrecoMedio_DeveRetornarCorreto()
    {
        // Arrange
        var operacoes = new List<PrecoMedioDto>
        {
            new PrecoMedioDto { Quantidade = 10, PrecoUnitario = 20 },
            new PrecoMedioDto { Quantidade = 5, PrecoUnitario = 40 }
        };

        // Act
        var resultado = _service.CalcularPrecoMedio(operacoes);

        // Assert
        Assert.Equal(26.67m, resultado);
    }

    [Fact]
    public void CalcularPrecoMedio_ComListaVazia_DeveLancarExcecao()
    {
        // Arrange
        var operacoes = new List<PrecoMedioDto>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.CalcularPrecoMedio(operacoes));
    }

    [Fact]
    public void CalcularPrecoMedio_ComValoresInvalidos_DeveLancarExcecao()
    {
        var operacoes = new List<PrecoMedioDto>
        {
            new PrecoMedioDto { Quantidade = 0, PrecoUnitario = 50}
        };

        Assert.Throws<ArgumentException>(() => _service.CalcularPrecoMedio(operacoes));
    }

    [Fact]
    public void CalcularPnL_DeveRetornarCorreto()
    {
        // Arrange
        var precoAtual = 30m;
        var precoMedio = 20m;
        var quantidade = 10;

        var resultado = _service.CalcularPnL(precoAtual, precoMedio, quantidade);

        Assert.Equal(100m, resultado);
    }
}
