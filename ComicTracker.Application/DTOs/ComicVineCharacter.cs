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
}