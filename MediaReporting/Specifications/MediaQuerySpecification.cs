using Lucene.Net.Search;
using MediaReporting.Extensions;
using MediaReporting.Messages.Query;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Models;

namespace MediaReporting.Specifications
{
    public class MediaQuerySpecification : SpecificationBase<IMedia>
    {
        private readonly MediaSearchFilter _filter;

        public MediaQuerySpecification(MediaSearchFilter filter)
        {
            _filter = filter;
        }

        // See MediaSizeSpecification for why filter size is not handled here
        public override Expression<Func<IMedia, bool>> ToExpression()
        {
            //These are not supported inline for some reason but "Contains" is
            var hasCreators = _filter.CreatorIds.Any();
            var hasMediaTypes = _filter.MediaTypeIds.Any();
            var deletedOnly = _filter.MediaStatus.ShowDeletedOnly();

            return mediaItem =>
                (!hasCreators || _filter.CreatorIds.Contains(mediaItem.CreatorId)) &&
                (!hasMediaTypes || _filter.MediaTypeIds.Contains(mediaItem.ContentType.Id)) &&
                (!deletedOnly.HasValue || mediaItem.Trashed == deletedOnly);
        }
    }
}
