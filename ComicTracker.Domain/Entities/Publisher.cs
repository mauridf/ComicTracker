namespace ComicTracker.Domain.Entities;

public class Publisher
{
    public int Id { get; set; }
    public int ComicVineId { get; set; }
    public string? Aliases { get; set; }
    public string? Deck { get; set; }
    public string? ImageUrl { get; set; }
    public string? LocationAddress { get; set; }
    public string? LocationCity { get; set; }
    public string? LocationState { get; set; }
    public string Name { get; set; }
    public string? SiteDetailUrl { get; set; }

    public ICollection<Volume> Volumes { get; set; }
    public ICollection<Character> Characters { get; set; }
    public ICollection<Team> Teams { get; set; }
}
