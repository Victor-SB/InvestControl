using InvestControl.Infrastructure.Context;
using InvestControl.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using InvestControl.Application.DTOs;

namespace InvestControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CotacoesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CotacoesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CotacaoDto>>> Get()
    {
        var cotacoes = await _context.Cotacoes
            .Include(c => c.Ativo)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable <CotacaoDto>>(cotacoes));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CotacaoDto>> GetById(int id)
    {
        var cotacao = await _context.Cotacoes
            .Include(c => c.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);

        return cotacao is null ? NotFound() : Ok(cotacao);
    }
    [HttpGet("ultimo/{codigo}")]
public async Task<ActionResult<CotacaoDto>> GetUltimaCotacaoPorCodigo(string codigo)
    {
        var cotacao = await _context.Cotacoes
            .Include(c => c.Ativo)
            .Where(c => c.Ativo.Codigo == codigo)
            .OrderByDescending(c => c.DataHora)
            .FirstOrDefaultAsync();

        if (cotacao is null)
            return NotFound($"Nenhuma cotação encontrada para o ativo {codigo}");

        return Ok(_mapper.Map<CotacaoDto>(cotacao));
    }
}