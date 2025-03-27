﻿namespace ComicTracker.Domain.Entities;
public class Team
{
    public int Id { get; set; }
    public int ComicVineId { get; set; }
    public string? Aliases { get; set; }
    public int? CountOfIssueAppearances { get; set; }
    public int? CountOfTeamMembers { get; set; }
    public string? Deck { get; set; }
    public string? Description { get; set; }
    public string? FirstAppearedInIssue { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; }
    public string? PublisherName { get; set; }
    public string? SiteDetailUrl { get; set; }

    public Publisher? Publisher { get; set; }
    public ICollection<Character> Characters { get; set; }
}