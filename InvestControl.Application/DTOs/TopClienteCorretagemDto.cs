using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Application.DTOs;

public class TopClienteCorretagemDto
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalCorretagem { get; set; }
}
