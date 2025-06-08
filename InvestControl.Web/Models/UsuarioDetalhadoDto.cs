namespace InvestControl.Web.Models;

public class UsuarioDetalhadoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal PorcentagemCorretagem { get; set; }
}