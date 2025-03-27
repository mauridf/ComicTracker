using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineVolumeInfo
{
    public string Name { get; set; }
    [JsonPropertyName("site_detail_url")]
    public string SiteDetailUrl { get; set; }
}
