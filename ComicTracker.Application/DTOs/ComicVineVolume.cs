using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineVolume
{
    public string Aliases { get; set; }
    [JsonPropertyName("count_of_issues")]
    public int CountOfIssues { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("first_issue")]
    public ComicVineFirstIssue FirstIssue { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }
    [JsonPropertyName("last_issue")]
    public ComicVineLastIssue LastIssue { get; set; }
    public string Name { get; set; }
    public ComicVinePublisherInfo Publisher { get; set; }
    [JsonPropertyName("site_detail_url")]
    public string SiteDetailUrl { get; set; }
    [JsonPropertyName("start_year")]
    public int StartYear { get; set; }
}