namespace Blog.MVC.DTOs
{
    public class BlogPostDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;
    }
}
