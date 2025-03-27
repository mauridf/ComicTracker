using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVinePublisher
{
    public string Aliases { get; set; }
    public string Deck { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }

    [JsonPropertyName("location_address")]
    public string LocationAddress { get; set; }

    [JsonPropertyName("location_city")]
    public string LocationCity { get; set; }

    [JsonPropertyName("location_state")]
    public string LocationState { get; set; }

    public string Name { get; set; }

    [JsonPropertyName("site_detail_url")]
    public string SiteDetailUrl { get; set; }
}
