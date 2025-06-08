using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Web.Models;

public class AtivoDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}
