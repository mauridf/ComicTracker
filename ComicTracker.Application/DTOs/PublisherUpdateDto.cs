namespace ComicTracker.Application.DTOs;

public class PublisherUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Aliases { get; set; }
    public string Deck { get; set; }
    public string ImageUrl { get; set; }
    public string LocationAddress { get; set; }
    public string LocationCity { get; set; }
    public string LocationState { get; set; }
    public string SiteDetailUrl { get; set; }
}
