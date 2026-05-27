namespace DormitoryManagement.Domain.Common
{
    /// <summary>
    /// Lớp cơ sở chứa các thông tin phân trang.
    /// </summary>
    public class BasePagedResult
    {
        /// <summary>
        /// Tổng số lượng bản ghi có trong hệ thống.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Số thứ tự của trang hiện tại (bắt đầu từ 1).
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Kích thước của mỗi trang (số lượng bản ghi trên một trang).
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Tổng số trang dựa trên tổng số lượng bản ghi và kích thước trang.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Kiểm tra xem có trang trước đó hay không.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Kiểm tra xem có trang tiếp theo hay không.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }

    /// <summary>
    /// Lớp đại diện cho kết quả phân trang trả về kèm theo danh sách các phần tử.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của phần tử trong danh sách.</typeparam>
    public class PagedResult<T> : BasePagedResult
    {
        /// <summary>
        /// Danh sách các phần tử của trang hiện tại.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp <see cref="PagedResult{T}"/>.
        /// </summary>
        public PagedResult() { Items = new List<T>(); }

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp <see cref="PagedResult{T}"/> với các giá trị được chỉ định.
        /// </summary>
        /// <param name="items">Danh sách phần tử.</param>
        /// <param name="totalCount">Tổng số bản ghi.</param>
        /// <param name="pageNumber">Trang hiện hành.</param>
        /// <param name="pageSize">Số bản ghi trên trang.</param>
        public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
