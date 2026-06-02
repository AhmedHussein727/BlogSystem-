using Blog.MVC.DTOs;

namespace Blog.MVC.ViewModels
{
    public class PostsIndexViewModel
    {
        public IEnumerable<BlogPostDto> Posts { get; set; }
            = Enumerable.Empty<BlogPostDto>();

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }
    }
}
