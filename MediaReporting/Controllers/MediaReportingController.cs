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

namespace MediaReporting.Controllers
{
    [PluginController("MediaReporting")]
    //[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
    public class MediaReportingController : UmbracoApiController
    {
        private readonly IMediaService _mediaService;
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IUserService _userService;
        private readonly IUmbracoDatabaseFactory _umbracoDatabaseFactory;
        private readonly IMediaReportHelper _mediaReportHelper;

        public MediaReportingController(
            IMediaService mediaService,
            IUserService userService,
            IUmbracoDatabaseFactory umbracoDatabaseFactory,
            IMediaTypeService mediaTypeService,
            IMediaReportHelper mediaReportHelper
        )
        {
            _mediaService = mediaService;
            _userService = userService;
            _umbracoDatabaseFactory = umbracoDatabaseFactory;
            _mediaTypeService = mediaTypeService;
            _mediaReportHelper = mediaReportHelper;
        }


        [HttpGet]
        public IActionResult GetAllMediaSizes([FromQuery] MediaSizeFilter filter, [FromQuery] Pagination pagination)
        {
            var mediaFilterSpecification = new MediaQuerySpecification(filter);

            IQuery<IMedia> query = _umbracoDatabaseFactory.SqlContext
                .Query<IMedia>()
                .Where(mediaFilterSpecification.ToExpression())
                .Where(x => x.Properties.Any());

            var records = _mediaService.GetPagedDescendants(-1, pagination.PageIndex, pagination.PageSize, out long totalRecords, query);

            var mapped = records.Select(mediaItem => {
                var creator = _userService
                    .GetUserById(mediaItem.CreatorId)
                    .ThrowIfNull($"The creator of media item {mediaItem.Id} could not be found");
                return new MediaItemView(creator.Username, mediaItem);
            })
            .ToPaginatedView(totalRecords, pagination);

            return Ok(mapped);
        }


        [HttpGet]
        public IActionResult GetAllMediaSizesCSV([FromQuery] MediaSizeFilter filter)
        {
            var media = _mediaService.GetRootMedia();
            var results = _mediaReportHelper.GetMediaForFolder(media, filter);
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
