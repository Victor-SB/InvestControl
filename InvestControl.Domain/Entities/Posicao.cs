using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Domain.Entities;

public class Posicao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int AtivoId { get; set; }
    public Ativo Ativo { get; set; } = null!;

    public int Quantidade { get; set; }
    public decimal PrecoMedio { get; set; }
    public decimal PnL { get; set; }
}
