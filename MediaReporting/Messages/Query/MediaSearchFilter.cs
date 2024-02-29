

namespace MediaReporting.Messages.Query
{
    public class MediaSearchFilter
    {
        public MediaSearchFilter()
        {
            CreatorIds = new List<int>();
            MediaStatus = new List<MediaStatus>();
            MediaTypeIds = new List<int>();
        }

        public IEnumerable<int> CreatorIds { get; set; }
        public IEnumerable<MediaStatus> MediaStatus { get; set; }
        public IEnumerable<int> MediaTypeIds { get; set; }
        public int? MinimumSize { get; set; }
    }
}
