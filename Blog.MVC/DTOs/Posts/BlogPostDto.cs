using Blog.Domain.Entities.Enums;

namespace Blog.MVC.DTOs.Posts
{
    public class BlogPostDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = default!;
        public string AuthorName { get; set; } = default!;

        public Status Status { get; set; }
    }
}
