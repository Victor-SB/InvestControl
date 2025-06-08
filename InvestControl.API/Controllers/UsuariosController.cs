using AutoMapper;
using InvestControl.Application.DTOs;
using InvestControl.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UsuariosController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDetalhadoDto>> GetUsuarioById(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
            return NotFound($"Usuário com ID {id} não encontrado.");

        var usuarioDto = _mapper.Map<UsuarioDetalhadoDto>(usuario);
        return Ok(usuarioDto);
    }
}
