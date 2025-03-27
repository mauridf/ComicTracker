namespace ComicTracker.Application.DTOs;

public class ComicVineTeam
{
    public string Aliases { get; set; }
    public int CountOfIssueAppearances { get; set; }
    public int CountOfTeamMembers { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    public ComicVineFirstAppearedInIssue FirstAppearedInIssue { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }
    public string Name { get; set; }
    public ComicVinePublisherInfo Publisher { get; set; }
    public string SiteDetailUrl { get; set; }
}