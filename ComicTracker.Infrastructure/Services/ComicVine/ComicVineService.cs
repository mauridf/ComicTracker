using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using System.Net.Http.Json;

namespace ComicTracker.Infrastructure.Services.ComicVine;

public class ComicVineService : IComicVineService
{
    private readonly HttpClient _httpClient;
    private readonly ComicVineSettings _settings;

    public ComicVineService(HttpClient httpClient, ComicVineSettings settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public async Task<ComicVineResponse<ComicVinePublisher>> GetPublishers(string filter)
    {
        var url = $"publishers/?api_key={_settings.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVinePublisher>>(url);
    }

    // Implemente outros métodos conforme necessário
}