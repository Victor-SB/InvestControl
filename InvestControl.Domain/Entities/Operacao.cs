using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Domain.Entities;

public class Operacao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public int AtivoId { get; set; }
    public Ativo Ativo { get; set; } = null!;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public string TipoOperacao { get; set; } = null!; //COMPRA OU VENDA
    public decimal Corretagem { get; set; }
    public DateTime DataHora { get; set; }
}
