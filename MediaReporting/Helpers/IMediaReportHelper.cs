using MediaReporting.Messages.Query;
using MediaReporting.Messages.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;

namespace MediaReporting.Helpers
{
    public interface IMediaReportHelper
    {
        IEnumerable<MediaItemView> GetMediaForFolder(IEnumerable<IMedia> media, MediaSearchFilter filter, string host);
        FileContentResult GenerateCsv(IEnumerable<MediaItemView> results);
    }
}
