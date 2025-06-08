using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Application.DTOs;

public class CotacaoDto
{
    public int Id { get; set; }
    public int AtivoId { get; set; }
    public decimal PrecoUnitario { get; set; }
    public DateTime DataHora { get; set; }

    public AtivoDto? Ativo { get; set; }
}
