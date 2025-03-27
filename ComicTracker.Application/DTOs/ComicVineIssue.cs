namespace ComicTracker.Application.DTOs;

public class ComicVineIssue
{
    public string Aliases { get; set; }
    public DateTime? CoverDate { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    public bool? HasStaffReview { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }
    public int IssueNumber { get; set; }
    public string Name { get; set; }
    public string SiteDetailUrl { get; set; }
    public DateTime? StoreDate { get; set; }
    public ComicVineVolumeInfo Volume { get; set; }
}
