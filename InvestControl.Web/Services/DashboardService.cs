using System.Net.Http.Json;
using InvestControl.Web.Models;

namespace InvestControl.Web.Services;

public class DashboardService
{
    private readonly HttpClient _http;

    public DashboardService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PrecoMedioDto>> ObterPrecoMedioPorUsuarioAsync(int usuarioId)
    {
        return await _http.GetFromJsonAsync<List<PrecoMedioDto>>($"posicoes/preco-medio-por-usuario/{usuarioId}") ?? new();
    }

    public async Task<List<PosicaoDto>> ObterPosicoesPorUsuarioAsync(int usuarioId)
    {
        return await _http.GetFromJsonAsync<List<PosicaoDto>>($"posicoes/por-usuario/{usuarioId}") ?? new();
    }

    public async Task<decimal> ObterPnLGlobalAsync(int usuarioId)
    {
        return await _http.GetFromJsonAsync<decimal>($"posicoes/pnl-global/{usuarioId}");
    }

    public async Task<decimal> ObterTotalCorretagemAsync(int usuarioId)
    {
        return await _http.GetFromJsonAsync<decimal>($"operacoes/corretagem-por-usuario/{usuarioId}");
    }
}
