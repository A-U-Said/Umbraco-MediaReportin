using MediaReporting.Messages.Query;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Querying;
using Umbraco.Cms.Core.Services;

namespace MediaReporting.Data
{
    public interface IMediaQueries
    {
        Task<IEnumerable<dynamic>> GetPagedDescendants(Pagination pagination, MediaSearchFilter filter);
    }
}
