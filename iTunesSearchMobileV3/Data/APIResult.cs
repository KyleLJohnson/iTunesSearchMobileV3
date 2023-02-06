using System.ComponentModel.DataAnnotations;

namespace iTunesSearchMobileV3.Data
{
    internal class APIResult
    {
        public int ResultCount { get; set; }

        public SearchResult[]? Results { get; set; }


    }
    /// <summary>
    /// Search Result Schema/Model
    /// </summary>
    public class SearchResult
    {
        [Key]
        public int TrackId { get; set; }

        public string? ArtistName { get; set; }

        public string? CollectionName { get; set; }

        public string? TrackName { get; set; }
        public string? ArtworkUrl100 { get; set; }
        public int? ClickCount { get; set; }
        public string? TrackViewUrl { get; set; }
    }
}
