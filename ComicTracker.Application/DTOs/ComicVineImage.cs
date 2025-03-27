using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineImage
{
    [JsonPropertyName("icon_url")]
    public string IconUrl { get; set; }

    [JsonPropertyName("medium_url")]
    public string MediumUrl { get; set; }

    [JsonPropertyName("original_url")]
    public string OriginalUrl { get; set; }

    [JsonPropertyName("thumb_url")]
    public string ThumbUrl { get; set; }

    [JsonPropertyName("screen_large_url")]
    public string ScreenLargeUrl { get; set; }
}
