using InvestControl.Web.Models;
using System.Net.Http.Json;

namespace InvestControl.Web.Services;

public class UsuarioService
{
    private readonly HttpClient _http;

    public UsuarioService(HttpClient http)
    {
        _http = http;
    }

    public async Task<UsuarioDetalhadoDto?> ObterPorIdAsync(int usuarioId)
    {
        try
        {
            return await _http.GetFromJsonAsync<UsuarioDetalhadoDto>($"https://localhost:5024/api/usuarios/{usuarioId}");
        }
        catch
        {
            return null;
        }
    }
}
