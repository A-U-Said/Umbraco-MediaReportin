using MediaReporting.Messages.Query;
using MediaReporting.Messages.View;


namespace MediaReporting.Extensions
{
    public static class EnumerableExtensions
    {
        public static PagedCollectionView<T> ToPaginatedView<T>(this IEnumerable<T> source, long totalRecords, Pagination pagination)
        {
            return new PagedCollectionView<T>(source.ToList(), pagination, totalRecords);
        }
    }
}
