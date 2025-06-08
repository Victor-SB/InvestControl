using InvestControl.Web.Models;
using System.Net.Http.Json;

namespace InvestControl.Web.Services;

public class OperacaoService
{
    private readonly HttpClient _http;

    public OperacaoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<decimal> ObterTotalCorretagemPorUsuarioAsync(int usuarioId)
    {
        var url = $"https://localhost:5024/api/operacoes/corretagens?usuarioId={usuarioId}";
        return await _http.GetFromJsonAsync<decimal>(url);
    }
}
