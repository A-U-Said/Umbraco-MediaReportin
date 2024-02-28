using MediaReporting.Messages.Query;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Querying;
using Umbraco.Cms.Core.Services;

namespace MediaReporting.Data
{
    public interface IMediaQueries
    {
        IEnumerable<IMedia> GetPagedDescendants(Pagination pagination, out long totalRecords, IQuery<IMedia>? filter = null, Ordering? ordering = null);
    }
}
