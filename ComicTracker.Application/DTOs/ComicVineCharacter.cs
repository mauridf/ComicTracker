using System.Text.Json.Serialization;

namespace ComicTracker.Application.DTOs.ComicVine
{
    public class ComicVineCharacter
    {
        public string Aliases { get; set; }
        public string Birth { get; set; }
        [JsonPropertyName("count_of_issue_appearances")]
        public int CountOfIssueAppearances { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("first_appeared_in_issue")]
        public ComicVineFirstAppearedInIssue FirstAppearedInIssue { get; set; }
        public int Gender { get; set; }
        public int Id { get; set; }
        public ComicVineImage Image { get; set; }
        public string Name { get; set; }
        public ComicVineOrigin Origin { get; set; }
        public ComicVinePublisherInfo Publisher { get; set; }
        [JsonPropertyName("real_name")]
        public string RealName { get; set; }
        [JsonPropertyName("site_detail_url")]
        public string SiteDetailUrl { get; set; }
    }
}