using ComicTracker.Application.DTOs;
using ComicTracker.Application.DTOs.ComicVine;
using ComicTracker.Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace ComicTracker.Infrastructure.Services.ComicVine;

public class ComicVineService : IComicVineService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<ComicVineSettings> _settings;

    // Use IOptions<ComicVineSettings> em vez de ComicVineSettings diretamente
    public ComicVineService(HttpClient httpClient, IOptions<ComicVineSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);

        // Adicione estes headers
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ComicTracker/1.0");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<ComicVineResponse<ComicVinePublisher>> GetPublishers(string filter)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                PropertyNameCaseInsensitive = true
            };

            var url = $"publishers/?api_key={_settings.Value.ApiKey}&format=json&filter={filter}";
            var response = await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVinePublisher>>(url, options);

            return response;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            throw new Exception("Acesso negado. Verifique sua chave de API da Comic Vine.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao acessar a API da Comic Vine", ex);
        }
    }

    public async Task<ComicVineResponse<ComicVineCharacter>> GetCharacters(string filter)
    {
        var url = $"characters/?api_key={_settings.Value.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineCharacter>>(url);
    }

    public async Task<ComicVineResponse<ComicVineTeam>> GetTeams(string filter)
    {
        var url = $"teams/?api_key={_settings.Value.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineTeam>>(url);
    }

    public async Task<ComicVineResponse<ComicVineVolume>> GetVolumes(string filter)
    {
        var url = $"volumes/?api_key={_settings.Value.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineVolume>>(url);
    }

    public async Task<ComicVineResponse<ComicVineIssue>> GetIssues(string filter)
    {
        var url = $"issues/?api_key={_settings.Value.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineIssue>>(url);
    }

    public async Task<ComicVineResponse<ComicVineIssue>> GetIssuesByVolumeId(int volumeId)
    {
        var url = $"issues/?api_key={_settings.Value.ApiKey}&format=json&filter=volume:{volumeId}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineIssue>>(url);
    }

    public async Task<ComicVineResponse<ComicVineIssue>> GetIssuesByVolume(int volumeId)
    {
        var url = $"issues/?api_key={_settings.Value.ApiKey}&format=json&filter=volume:{volumeId}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineIssue>>(url);
    }
}