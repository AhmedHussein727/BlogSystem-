namespace Blog.MVC.DTOs.Account
{
    public class PaginationResponse<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public List<T> Data { get; set; } = [];
    }
}
