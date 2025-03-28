namespace ComicTracker.Application.DTOs;

public class TeamUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Aliases { get; set; }
    public int? CountOfIssueAppearances { get; set; }
    public int? CountOfTeamMembers { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    public string FirstAppearedInIssue { get; set; }
    public string ImageUrl { get; set; }
    public string PublisherName { get; set; }
    public string SiteDetailUrl { get; set; }
}