using AutoMapper;
using InvestControl.Application.DTOs;
using InvestControl.Domain.Entities;
using InvestControl.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Ativos")]
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
    public async Task<ActionResult<AtivoDto>> GetById(int id)
    {
        var ativo = await _context.Ativos.FindAsync(id);
        if (ativo is null) return NotFound();

        return Ok(_mapper.Map<AtivoDto>(ativo));
    }

    [HttpPost]
    public async Task<ActionResult<AtivoDto>> Create(AtivoDto dto)
    {
        var novoAtivo = _mapper.Map<Ativo>(dto);
        _context.Ativos.Add(novoAtivo);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<AtivoDto>(novoAtivo);
        return CreatedAtAction(nameof(GetById), new { id = novoAtivo.Id }, resultDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AtivoDto dto)
    {
        var existente = await _context.Ativos.FindAsync(id);
        if (existente is null) return NotFound();

        // Atualiza os campos necessários
        existente.Codigo = dto.Codigo;
        existente.Nome = dto.Nome;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ativo = await _context.Ativos.FindAsync(id);
        if (ativo is null) return NotFound();

        _context.Ativos.Remove(ativo);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
