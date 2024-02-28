using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;

namespace MediaReporting.Extensions
{
    public static class ContentExtensions
    {
        public static int GetFileSize(this IMedia mediaItem)
        {
            if (!mediaItem.HasProperty(Constants.Conventions.Media.Bytes))
            {
                return int.MaxValue; 
            }

            return mediaItem.GetValue<int>(Constants.Conventions.Media.Bytes);
        }
    }
}
