namespace MediaReporting.Messages.Query
{
    public class Pagination
    {
        public Pagination()
        {
        }

        public long PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 25;
    }
}
