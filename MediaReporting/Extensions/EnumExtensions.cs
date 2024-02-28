using MediaReporting.Messages.Query;

namespace MediaReporting.Extensions
{
    public static class EnumExtensions
    {
        public static bool? ShowDeletedOnly(this IEnumerable<MediaStatus> mediaStatus)
        {
            if (
                (mediaStatus.Contains(MediaStatus.Active) && mediaStatus.Contains(MediaStatus.Deleted)) ||
                !mediaStatus.Any()
            )
            {
                return null;
            }
            else if (mediaStatus.Contains(MediaStatus.Deleted))
            {
                return true;
            }
            return false;
        }
    }
}
