namespace ComicTracker.Application.DTOs;

public class ComicVinePublisher
{
    public string Aliases { get; set; }
    public string Deck { get; set; }
    public int Id { get; set; }
    public ComicVineImage Image { get; set; }
    public string LocationAddress { get; set; }
    public string LocationCity { get; set; }
    public string LocationState { get; set; }
    public string Name { get; set; }
    public string SiteDetailUrl { get; set; }
}

public class ComicVineImage
{
    public string IconUrl { get; set; }
    public string MediumUrl { get; set; }
    public string OriginalUrl { get; set; }
    public string ThumbUrl { get; set; }
    public string SmallUrl { get; set; }
    public string SuperUrl { get; set; }
    public string ScreenUrl { get; set; }
    public string ScreenLargeUrl { get; set; }
}
