namespace ComicTracker.Application.DTOs;

public class IssueUpdateDto
{
    public int Id { get; set; }
    public string Aliases { get; set; }
    public DateTime? CoverDate { get; set; }
    public string Deck { get; set; }
    public string Description { get; set; }
    public bool? HasStaffReview { get; set; }
    public string ImageUrl { get; set; }
    public int IssueNumber { get; set; }
    public string Name { get; set; }
    public string SiteDetailUrl { get; set; }
    public DateTime? StoreDate { get; set; }
    public string VolumeDetails { get; set; }
    public bool Read { get; set; }
    public string Notes { get; set; }
    public int? Rating { get; set; }
}
