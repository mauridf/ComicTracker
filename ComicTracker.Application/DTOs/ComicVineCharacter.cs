namespace ComicTracker.Application.DTOs.ComicVine
{
    public class ComicVineCharacter
    {
        public string Aliases { get; set; }
        public string Birth { get; set; }
        public int CountOfIssueAppearances { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        public ComicVineFirstAppearedInIssue FirstAppearedInIssue { get; set; }
        public int Gender { get; set; }
        public int Id { get; set; }
        public ComicVineImage Image { get; set; }
        public string Name { get; set; }
        public ComicVineOrigin Origin { get; set; }
        public ComicVinePublisherInfo Publisher { get; set; }
        public string RealName { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class ComicVineFirstAppearedInIssue
    {
        public string Name { get; set; }
        public int IssueNumber { get; set; }
    }

    public class ComicVineImage
    {
        public string IconUrl { get; set; }
        public string MediumUrl { get; set; }
        public string OriginalUrl { get; set; }
        public string ThumbUrl { get; set; }
    }

    public class ComicVineOrigin
    {
        public string Name { get; set; }
    }

    public class ComicVinePublisherInfo
    {
        public string Name { get; set; }
    }
}