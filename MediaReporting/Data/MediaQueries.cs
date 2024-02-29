using MediaReporting.Messages.Query;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Persistence.Repositories;
using NPoco;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;


namespace MediaReporting.Data
{
    public class MediaQueries : IMediaQueries
    {
        private readonly IScopeProvider _scopeProvider;

        public MediaQueries(
            IScopeProvider scopeProvider
        )
        {
            _scopeProvider = scopeProvider;
        }

        public async Task<IEnumerable<dynamic>> GetPagedDescendants(Pagination pagination, MediaSearchFilter filter)
        {
            if (pagination.PageIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pagination.PageIndex));
            }

            if (pagination.PageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pagination.PageSize));
            }

            var ordering = Ordering.By("Path");

            using var scope = _scopeProvider.CreateScope();
            scope.ReadLock(Constants.Locks.MediaTree);

            Sql<ISqlContext>? sql = scope.SqlContext?.Sql()?
                .Select<ContentDto>()
                .AndSelect<ContentVersionDto>(x => x.Id)
                .AndSelect(
                    "umbracoPropertyData.versionId", 
                    "umbracoPropertyData.propertyTypeId",
                    "umbracoPropertyData.languageId",
                    "umbracoPropertyData.segment",
                    "umbracoPropertyData.intValue",
                    "umbracoPropertyData.decimalValue",
                    "umbracoPropertyData.dateValue",
                    "umbracoPropertyData.varcharValue",
                    "umbracoPropertyData.textValue"
                 )
                .From<ContentDto>()
                .InnerJoin<ContentVersionDto>()
                .On<ContentDto, ContentVersionDto>((x, y) => y.NodeId == x.NodeId)
                .InnerJoin(Constants.DatabaseSchema.Tables.PropertyData)
                .On("umbracoPropertyData.VersionId = umbracoContentVersion.Id")
                .Where<ContentDto>(x => x.ContentTypeId == 1032);

            var queryResults = await scope.Database.FetchAsync<dynamic>(sql);
            scope.Complete();

            return queryResults;
        }
    }
}
