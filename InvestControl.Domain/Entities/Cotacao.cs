using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Domain.Entities;

public class Cotacao
{
    public int Id { get; set; }
    public int AtivoId { get; set; }
    public Ativo Ativo { get; set; } = null!;

    public decimal PrecoUnitario { get; set; }
    public DateTime DataHora { get; set; }

}
