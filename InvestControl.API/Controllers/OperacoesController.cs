using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestControl.Infrastructure.Context;
using InvestControl.Application.DTOs;

namespace InvestControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public OperacoesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("corretagem/{usuarioId}")]
    public async Task<ActionResult<decimal>> GetTotalCorretagem(int usuarioId)
    {
        var total = await _context.Operacoes
            .Where(o => o.UsuarioId == usuarioId)
            .SumAsync(o => o.Corretagem);

        return Ok(Math.Round(total, 2));
    }

    [HttpGet("top-clientes-corretagem")]
    public async Task<ActionResult<IEnumerable<TopClienteCorretagemDto>>> GetTopClientesQueMaisPagaramCorretagem()
    {
        var topClientes = await _context.Operacoes
            .Include(o => o.Usuario)
            .GroupBy(o => new { o.UsuarioId, o.Usuario.Nome })
            .Select(g => new TopClienteCorretagemDto
            {
                UsuarioId = g.Key.UsuarioId,
                Nome = g.Key.Nome,
                TotalCorretagem = g.Sum(o => o.Corretagem)
            })
            .OrderByDescending(c => c.TotalCorretagem)
            .Take(10)
            .ToListAsync();

        return Ok(topClientes);
    }
}
