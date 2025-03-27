using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineResponse<T>
{
    public string Error { get; set; }
    [JsonPropertyName("number_of_page_results")]
    public int NumberOfPageResults { get; set; }
    [JsonPropertyName("number_of_total_results")]
    public int NumberOfTotalResults { get; set; }
    public List<T> Results { get; set; }
}