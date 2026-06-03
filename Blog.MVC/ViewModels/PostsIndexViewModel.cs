using Blog.MVC.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.MVC.ViewModels
{
    public class PostsIndexViewModel
    {
        public IEnumerable<BlogPostDto> Posts { get; set; }
            = Enumerable.Empty<BlogPostDto>();
        public IEnumerable<SelectListItem> Categories
    = Enumerable.Empty<SelectListItem>();

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public string? CategoryName { get; set; }

        public string? Status { get; set; }
    }
}
