using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineIssue
{
    public string Aliases { get; set; }
    [JsonPropertyName("cover_date")]
    public DateTime? CoverDate { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("has_staff_review")]
    public bool? HasStaffReview { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }
    [JsonPropertyName("issue_number")]
    public int IssueNumber { get; set; }
    public string Name { get; set; }
    [JsonPropertyName("site_detail_url")]
    public string SiteDetailUrl { get; set; }
    [JsonPropertyName("store_date")]
    public DateTime? StoreDate { get; set; }
    public ComicVineVolumeInfo Volume { get; set; }
}
