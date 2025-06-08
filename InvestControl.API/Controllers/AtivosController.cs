using AutoMapper;
using InvestControl.Application.DTOs;
using InvestControl.Domain.Entities;
using InvestControl.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtivosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AtivosController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AtivoDto>>> GetAll()
    {
        var ativos = await _context.Ativos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<AtivoDto>>(ativos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ativo>> GetById(int id)
    {
        var ativo = await _context.Ativos.FindAsync(id);
        return ativo is null ? NotFound() : Ok(ativo);
    }

    [HttpPost]
    public async Task<ActionResult<Ativo>> Create(Ativo ativo)
    {
        _context.Ativos.Add(ativo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = ativo.Id }, ativo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Ativo ativo)
    {
        if (id != ativo.Id) return BadRequest();

        var existente = await _context.Ativos.FindAsync(id);
        if (existente is null) return NotFound();

        existente.Codigo = ativo.Codigo;
        existente.Nome = ativo.Nome;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id)
    {
        var ativo = await _context.Ativos.FindAsync(id);
        if (ativo is null) return NotFound();

        _context.Ativos.Remove(ativo);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
