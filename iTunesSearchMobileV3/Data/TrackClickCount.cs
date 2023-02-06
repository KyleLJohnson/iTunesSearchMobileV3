using System.ComponentModel.DataAnnotations;

namespace iTunesSearchMobileV3.Data
{
    public class TrackClickCount
    {
        [Key]
        public int TrackId { get; set; }
        public int ClickCount { get; set; }
    }
}
