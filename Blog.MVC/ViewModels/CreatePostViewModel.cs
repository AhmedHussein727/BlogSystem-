using Blog.MVC.DTOs.Posts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.MVC.ViewModels
{
    public class CreatePostViewModel
    {
        public CreateBlogPostDTO Post { get; set; } = new();

        public IEnumerable<SelectListItem> Categories
            = Enumerable.Empty<SelectListItem>();

    }
}
