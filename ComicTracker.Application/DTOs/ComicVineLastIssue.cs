using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineLastIssue
{
    [JsonPropertyName("issue_number")]
    public int IssueNumber { get; set; }
}
