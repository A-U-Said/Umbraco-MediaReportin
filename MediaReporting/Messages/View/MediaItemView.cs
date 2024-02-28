using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;


namespace MediaReporting.Messages.View
{
    public class MediaItemView
    {
        public MediaItemView()
        {
        }

        public MediaItemView(string creator, IMedia mediaItem)
        {
            Creator = creator;
            CreatedDate = mediaItem.CreateDate;
            FileSize = mediaItem.GetValue<int>(Constants.Conventions.Media.Bytes);
            MediaType = mediaItem.ContentType.Alias;
            FileName = mediaItem.Name;
            ImageWidth = mediaItem.GetValue<int?>(Constants.Conventions.Media.Width);
            ImageHeight = mediaItem.GetValue<int?>(Constants.Conventions.Media.Height);
            IsInBin = mediaItem.Trashed;
            Url = mediaItem.GetValue<string?>(Constants.Conventions.Media.File);
            BackofficeUrl = $"https://lionhearttrust.org.uk/lionheart-cms#/media/media/edit/{mediaItem.Id}";
        }


        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FileSize { get; set; }
        public string MediaType { get; set; }
        public string? FileName { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public bool IsInBin { get; set; }
        public string? Url { get; set; }
        public string BackofficeUrl { get; set; }
    }
}