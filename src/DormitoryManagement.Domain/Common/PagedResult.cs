namespace DormitoryManagement.Domain.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        
        public int TotalCount { get; set; }
        
        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }

        // Thêm các thuộc tính bổ trợ (tùy chọn)
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;

        public bool HasPreviousPage => PageNumber > 1;
        
        public bool HasNextPage => PageNumber < TotalPages;

        public PagedResult() { }

        public PagedResult(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
