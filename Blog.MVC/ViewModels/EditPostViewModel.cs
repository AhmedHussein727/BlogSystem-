using Blog.MVC.DTOs.Posts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.MVC.ViewModels
{
    public class EditPostViewModel
    {
        public int Id { get; set; }

        public UpdatePostDto Post { get; set; } = new();

        public IEnumerable<SelectListItem> Categories
            = Enumerable.Empty<SelectListItem>();
    }
}
