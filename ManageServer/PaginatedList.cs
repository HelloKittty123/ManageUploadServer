namespace BE.Helper
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public PaginatedList(List<T> items, int count, int? pageIndex, int? pageSize)
        {
            if(pageIndex == null)
            {
                PageIndex = 1;
            }
            else
            {
                PageIndex = (int)pageIndex;
            }

            if (pageSize > 0 && pageSize != null)
            {
                TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            }
            else
            {
                TotalPage = count > 0 ? 1 : 0;
            }
            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPage;

        public static PaginatedList<T> Create(IQueryable<T> source, int? pageIndex, int? pageSize)
        {
            if(pageIndex == null || pageIndex == 0)
            {
                pageIndex = 1;
            }
            var count = source.Count();
            List<T> items;
            if(pageSize > 0)
            {
                items = source.Skip((int)((pageIndex - 1) * pageSize)).Take((int)pageSize).ToList();
            }
            else
            {
                items = source.ToList();
            }

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
