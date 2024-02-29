using MediaReporting.Messages.Query;


namespace MediaReporting.Messages.View
{
    public class PaginationView
    {
        public PaginationView()
        {

        }

        public PaginationView(Pagination pagination, long totalRecords)
        {
            PageIndex = pagination.PageIndex;
            PageSize = pagination.PageSize;
            TotalRecords = totalRecords;
        }

        public long PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }

        public long PageNumber => TotalPages == 0
            ? 0 
            : PageIndex + 1;
        public long TotalPages => (PageSize == 0 || TotalRecords == 0)
            ? 0
            : Convert.ToInt32(Math.Ceiling(TotalRecords / (decimal)PageSize));
    }
}
