using System.Net.Http.Json;
using InvestControl.Web.Models;

namespace InvestControl.Web.Services;

public class CotacoesService
{
    private readonly HttpClient _httpClient;

    public CotacoesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CotacaoDto>?> GetCotacoesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CotacaoDto>>("cotacoes");
    }

    public async Task<CotacaoDto?> GetUltimaCotacaoAsync(string codigoAtivo)
    {
        return await _httpClient.GetFromJsonAsync<CotacaoDto>($"cotacoes/ultima/{codigoAtivo}");
    }
}
