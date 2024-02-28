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
    }
}
