namespace ComicTracker.Application.DTOs;

public class VolumeUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Aliases { get; set; }
    public int? CountOfIssues { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    public int? FirstIssue { get; set; }
    public string ImageUrl { get; set; }
    public int? LastIssue { get; set; }
    public string SiteDetailUrl { get; set; }
    public int? StartYear { get; set; }
}
