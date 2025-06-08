using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Web.Models;

public class TopClientePosicaoDto
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
}
