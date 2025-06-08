namespace InvestControl.Web.Models;

public class PosicaoGlobalDto
{
    public decimal ValorInvestido { get; set; }
    public decimal ValorAtual { get; set; }
    public decimal Resultado { get; set; } // Lucro ou prejuízo
}