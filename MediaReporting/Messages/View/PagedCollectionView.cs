using MediaReporting.Messages.Query;

namespace MediaReporting.Messages.View
{
    public class PagedCollectionView<T>
    {
        public PagedCollectionView(ICollection<T> items, Pagination pageOptions, long totalRecords)
        {
            Items = items;
            Paging = new PaginationView(pageOptions, totalRecords);
        }

        public ICollection<T> Items { get; protected set; }
        public PaginationView Paging { get; protected set; }
    }
}
