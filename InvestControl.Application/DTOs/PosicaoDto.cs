using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Application.DTOs;

public class PosicaoDto
{
    public int Id { get; set; }
    public int AtivoId { get; set; }
    public int UsuarioId { get; set; }
    public decimal Quantidade { get; set; }
    public decimal PrecoMedio { get; set; }
    public decimal PnL { get; set; }

    public AtivoDto? Ativo { get; set; }
}
