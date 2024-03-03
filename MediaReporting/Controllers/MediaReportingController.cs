using MediaReporting.Helpers;
using MediaReporting.Extensions;
using MediaReporting.Messages.Query;
using MediaReporting.Messages.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Querying;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;
using MediaReporting.Specifications;
using Umbraco.Cms.Core;
using System.Linq;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Models.Membership;
using Microsoft.AspNetCore.Authorization;
using Polly;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace MediaReporting.Controllers
{
    [PluginController("MediaReporting")]
    //[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
    public class MediaReportingController : UmbracoAuthorizedApiController
    {
        private readonly IMediaService _mediaService;
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IUserService _userService;
        private readonly IUmbracoDatabaseFactory _umbracoDatabaseFactory;
        private readonly IUmbracoMapper _umbracoMapper;
        private readonly IMediaReportHelper _mediaReportHelper;

        public MediaReportingController(
            IMediaService mediaService,
            IUserService userService,
            IUmbracoDatabaseFactory umbracoDatabaseFactory,
            IUmbracoMapper umbracoMapper,
            IMediaTypeService mediaTypeService,
            IMediaReportHelper mediaReportHelper
        )
        {
            _mediaService = mediaService;
            _userService = userService;
            _umbracoDatabaseFactory = umbracoDatabaseFactory;
            _umbracoMapper = umbracoMapper;
            _mediaTypeService = mediaTypeService;
            _mediaReportHelper = mediaReportHelper;
        }


        [HttpGet]
        public IActionResult FilterMedia([FromQuery] string? searchTerm, [FromQuery] MediaSearchFilter filter, [FromQuery] Pagination pagination)
        {
            var mediaFilterSpecification = new MediaQuerySpecification(filter);

            IQuery<IMedia> query = _umbracoDatabaseFactory.SqlContext
                .Query<IMedia>()
                .Where(mediaFilterSpecification.ToExpression());

            // The IQuery implementation is awful and cannot call any extensions outside of scope
            // nor chain && statements or use ternaries
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                var stripped = rgx.Replace(searchTerm, " ");
                _ = int.TryParse(searchTerm, out int filterAsIntId);
                _ = Guid.TryParse(searchTerm, out Guid filterAsGuid);

                query = query
                    .Where(x => x.Name != null)
                    .Where(x => x.Name!.Contains(searchTerm) 
                        || x.Name!.Contains(stripped)
                        || x.Id == filterAsIntId 
                        || x.Key == filterAsGuid);
            }

            var records = _mediaService.GetPagedDescendants(-1, pagination.PageIndex, pagination.PageSize, out long totalRecords, query);
            var mapped = records
                .Where(new MediaSizeSpecification(filter.MinimumSize).ToExpression())
                .Select(mediaItem => {
                    var creator = _userService
                        .GetUserById(mediaItem.CreatorId)
                        .ThrowIfNull($"The creator of media item {mediaItem.Id} could not be found");
                    var creatorMapped = _umbracoMapper
                        .Map<IUser, UserBasic>(creator)
                        .ThrowIfNull($"The user could not be mapped to a view");
                    return new MediaItemView(creatorMapped, mediaItem, Request.Host.Value);
                })
            .ToPaginatedView(totalRecords, pagination);

            return Ok(mapped);
        }


        [HttpGet]
        public IActionResult ExportFilteredMedia([FromQuery] MediaSearchFilter filter)
        {
            var media = _mediaService.GetRootMedia();
            var results = _mediaReportHelper.GetMediaForFolder(media, filter, Request.Host.Value);
            return _mediaReportHelper.GenerateCsv(results);
        }


        [HttpGet]
        public IActionResult GetMediaTypes()
        {
            var content = _mediaTypeService.GetAll();
            var mapped = content.Select(mediaType => new MediaTypeView(mediaType));
            return Ok(mapped);
        }
    }
}
