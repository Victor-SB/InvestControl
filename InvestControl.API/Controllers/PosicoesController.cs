using InvestControl.Infrastructure.Context;
using InvestControl.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using InvestControl.Application.DTOs;

namespace InvestControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PosicoesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PosicoesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PosicaoDto>>> GetAll()
    {
        var posicoes = await _context.Posicoes
            .Include(p => p.Ativo)
            .OrderBy(p => p.UsuarioId)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<PosicaoDto>>(posicoes));
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<PosicaoDto>>> GetPosicoesPorUsuario(int usuarioId)
    {
        var posicoes = await _context.Posicoes
            .AsNoTracking()
            .Include(p => p.Ativo)
            .Where(p => p.UsuarioId == usuarioId)
            .ToListAsync();

        if (!posicoes.Any())
            return NotFound($"Nenhuma posição encontrada para o usuário {usuarioId}");

        var posicoesDto = _mapper.Map<IEnumerable<PosicaoDto>>(posicoes);
        return Ok(posicoesDto);
    }

    [HttpGet("posicao-global/{usuarioId}")]
    public async Task<ActionResult<PosicaoGlobalDto>> GetPosicaoGlobalDoUsuario(int usuarioId)
    {
        var posicoes = await _context.Posicoes
            .Include(p => p.Ativo)
            .Where(p => p.UsuarioId == usuarioId)
            .ToListAsync();

        if (!posicoes.Any())
            return NotFound($"Nenhuma posição encontrada para o usuário {usuarioId}");

        var codigosAtivos = posicoes.Select(p => p.Ativo.Codigo).Distinct();

        var cotacoes = await _context.Cotacoes
            .Where(c => codigosAtivos.Contains(c.Ativo.Codigo))
            .GroupBy(c => c.Ativo.Codigo)
            .Select(g => g.OrderByDescending(c => c.DataHora).FirstOrDefault())
            .ToListAsync();

        decimal valorInvestido = 0;
        decimal valorAtual = 0;

        foreach (var posicao in posicoes)
        {
            var cotacao = cotacoes.FirstOrDefault(c => c.Ativo.Codigo == posicao.Ativo.Codigo);
            if (cotacao == null) continue;

            valorInvestido += posicao.PrecoMedio * posicao.Quantidade;
            valorAtual += cotacao.PrecoUnitario * posicao.Quantidade;
        }

        var dto = new PosicaoGlobalDto
        {
            ValorInvestido = Math.Round(valorInvestido, 2),
            ValorAtual = Math.Round(valorAtual, 2)
        };

        return Ok(dto);
    }

    [HttpGet("preco-medio/{usuarioId}/{codigoAtivo}")]
    public async Task<ActionResult<decimal>> GetPrecoMedioPorUsuarioEAtivo(int usuarioId, string codigoAtivo)
    {
        var operacoesCompra = await _context.Operacoes
            .AsNoTracking()
            .Include(o => o.Ativo)
            .Where(o => o.UsuarioId == usuarioId &&
                        o.Ativo.Codigo == codigoAtivo &&
                        o.TipoOperacao.ToUpper() == "COMPRA")
            .Select(o => new { o.PrecoUnitario, o.Quantidade })
            .ToListAsync();

        if (!operacoesCompra.Any())
            return NotFound($"Nenhuma operação de compra encontrada para o usuário {usuarioId} e ativo {codigoAtivo}");

        var totalInvestido = operacoesCompra.Sum(o => o.PrecoUnitario * o.Quantidade);
        var totalQuantidade = operacoesCompra.Sum(o => o.Quantidade);
        var resultado = totalInvestido / totalQuantidade;

        return Ok(Math.Round(resultado, 2));
    }

    [HttpGet("precos-medios/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<PrecoMedioDetalhadoDto>>> GetPrecosMediosPorUsuario(int usuarioId)
    {
        var operacoes = await _context.Operacoes
            .Include(o => o.Ativo)
            .Where(o => o.UsuarioId == usuarioId && o.TipoOperacao.ToUpper() == "COMPRA")
            .ToListAsync();

        if (!operacoes.Any())
            return NotFound($"Nenhuma operação encontrada para o usuário {usuarioId}");

        var resultado = operacoes
            .GroupBy(o => o.Ativo.Codigo)
            .Select(g => new PrecoMedioDetalhadoDto
            {
                Codigo = g.Key,
                PrecoMedio = Math.Round(
                    g.Sum(o => o.PrecoUnitario * o.Quantidade) / g.Sum(o => o.Quantidade), 2)
            })
            .ToList();

        return Ok(resultado);
    }


    [HttpGet("top-clientes-posicao")]
    public async Task<ActionResult<IEnumerable<TopClientePosicaoDto>>> GetTopClientesComMaioresPosicoes()
    {
        var topClientes = await _context.Posicoes
            .Include(p => p.Ativo)
            .Include(p => p.Usuario)
            .GroupBy(p => new { p.UsuarioId, p.Usuario.Nome })
            .Select(g => new TopClientePosicaoDto
            {
                UsuarioId = g.Key.UsuarioId,
                Nome = g.Key.Nome,
                ValorTotal = g.Sum(p => p.Quantidade * p.PrecoMedio)
            })
            .OrderByDescending(c => c.ValorTotal)
            .Take(10)
            .ToListAsync();

        return Ok(topClientes);
    }
}