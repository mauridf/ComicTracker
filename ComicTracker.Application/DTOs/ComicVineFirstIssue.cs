using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs;

public class ComicVineFirstIssue
{
    [JsonPropertyName("issue_number")]
    public int IssueNumber { get; set; }
}
