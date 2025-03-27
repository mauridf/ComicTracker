using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineTeam
{
    public string Aliases { get; set; }
    [JsonPropertyName("count_of_isssue_appearances")]
    public int CountOfIssueAppearances { get; set; }
    [JsonPropertyName("count_of_team_members")]
    public int CountOfTeamMembers { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("first_appeared_in_issue")]
    public ComicVineFirstAppearedInIssue FirstAppearedInIssue { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }
    public string Name { get; set; }
    public ComicVinePublisherInfo Publisher { get; set; }
    [JsonPropertyName("site_detail_url")]
    public string SiteDetailUrl { get; set; }
}