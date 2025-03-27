namespace ComicTracker.Domain.Entities;
public class Character
{
    public int Id { get; set; }
    public int ComicVineId { get; set; }
    public string? Aliases { get; set; }
    public string? Birth { get; set; }
    public int? CountOfIssueAppearances { get; set; }
    public string? Deck { get; set; }
    public string? Description { get; set; }
    public string? FirstAppearedInIssue { get; set; }
    public int? Gender { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; }
    public string? Origin { get; set; }
    public string? PublisherName { get; set; }
    public string? RealName { get; set; }
    public string? SiteDetailUrl { get; set; }

    public Publisher? Publisher { get; set; }
    public ICollection<Volume> Volumes { get; set; }
}