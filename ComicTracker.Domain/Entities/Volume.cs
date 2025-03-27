namespace ComicTracker.Domain.Entities;
public class Volume
{
    public int Id { get; set; }
    public int ComicVineId { get; set; }
    public string? Aliases { get; set; }
    public int? CountOfIssues { get; set; }
    public string? Deck { get; set; }
    public string? Description { get; set; }
    public int? FirstIssue { get; set; }
    public string? ImageUrl { get; set; }
    public int? LastIssue { get; set; }
    public string Name { get; set; }
    public string? PublisherName { get; set; }
    public string? SiteDetailUrl { get; set; }
    public int? StartYear { get; set; }

    public Publisher? Publisher { get; set; }
    public ICollection<Issue> Issues { get; set; }
}