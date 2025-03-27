using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineFirstAppearedInIssue
{
    public string Name { get; set; }
    [JsonPropertyName("issue_number")]
    public int IssueNumber { get; set; }
}
