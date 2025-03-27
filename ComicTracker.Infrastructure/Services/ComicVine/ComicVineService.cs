using ComicTracker.Application.DTOs;
using ComicTracker.Application.DTOs.ComicVine;
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
        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
    }

    public async Task<ComicVineResponse<ComicVinePublisher>> GetPublishers(string filter)
    {
        var url = $"publishers/?api_key={_settings.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVinePublisher>>(url);
    }

    public async Task<ComicVineResponse<ComicVineCharacter>> GetCharacters(string filter)
    {
        var url = $"characters/?api_key={_settings.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineCharacter>>(url);
    }

    public async Task<ComicVineResponse<ComicVineTeam>> GetTeams(string filter)
    {
        var url = $"teams/?api_key={_settings.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineTeam>>(url);
    }

    public async Task<ComicVineResponse<ComicVineVolume>> GetVolumes(string filter)
    {
        var url = $"volumes/?api_key={_settings.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineVolume>>(url);
    }

    public async Task<ComicVineResponse<ComicVineIssue>> GetIssues(string filter)
    {
        var url = $"issues/?api_key={_settings.ApiKey}&format=json&filter={filter}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineIssue>>(url);
    }

    public async Task<ComicVineResponse<ComicVineIssue>> GetIssuesByVolumeId(int volumeId)
    {
        var url = $"issues/?api_key={_settings.ApiKey}&format=json&filter=volume:{volumeId}";
        return await _httpClient.GetFromJsonAsync<ComicVineResponse<ComicVineIssue>>(url);
    }
}