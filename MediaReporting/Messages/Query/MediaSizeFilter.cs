

namespace MediaReporting.Messages.Query
{
    public class MediaSizeFilter
    {
        public MediaSizeFilter()
        {
            MediaStatus = new List<MediaStatus>();
        }

        public int? CreatorId { get; set; }
        public int? MinimumSize { get; set; }
        public IEnumerable<MediaStatus> MediaStatus { get; set; }
        public int? MediaTypeId { get; set; }
    }
}
