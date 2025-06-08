using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Application.DTOs;

public class UsuarioDetalhadoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal PorcentagemCorretagem { get; set; }
}
