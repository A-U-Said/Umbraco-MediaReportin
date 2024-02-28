using MediaReporting.Extensions;
using MediaReporting.Messages.Query;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;

namespace MediaReporting.Specifications
{
    public class MediaQuerySpecification : SpecificationBase<IMedia>
    {
        private readonly MediaSizeFilter _filter;

        public MediaQuerySpecification(MediaSizeFilter filter)
        {
            _filter = filter;
        }


        public override Expression<Func<IMedia, bool>> ToExpression()
        {
            var deletedOnly = _filter.MediaStatus.ShowDeletedOnly();

            return mediaItem =>
                (!_filter.CreatorId.HasValue || _filter.CreatorId == mediaItem.CreatorId) &&
                (!_filter.MediaTypeId.HasValue || _filter.MediaTypeId == mediaItem.ContentType.Id) &&
                (!deletedOnly.HasValue || mediaItem.Trashed == deletedOnly);

        }
    }
}
