namespace ComicTracker.Application.DTOs;

public class ComicVineVolume
{
    public string Aliases { get; set; }
    public int CountOfIssues { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    public ComicVineFirstIssue FirstIssue { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }
    public ComicVineLastIssue LastIssue { get; set; }
    public string Name { get; set; }
    public ComicVinePublisherInfo Publisher { get; set; }
    public string SiteDetailUrl { get; set; }
    public int StartYear { get; set; }
}