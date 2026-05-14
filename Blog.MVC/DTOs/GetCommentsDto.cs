namespace Blog.MVC.DTOs
{
    public class GetCommentsDto
    {
        public int Id { get; set; }

        public string Content { get; set; } = default!;

        public string AuthorName { get; set; } = default!;
    }
}