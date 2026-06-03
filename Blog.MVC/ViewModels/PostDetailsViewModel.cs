using Blog.MVC.DTOs.Comments;
using Blog.MVC.DTOs.Posts;

namespace Blog.MVC.ViewModels
{
    public class PostDetailsViewModel
    {
        public BlogPostDto Post { get; set; } = default!;

        public List<GetCommentsDto> Comments { get; set; } = [];

        public CreateCommentDto CreateComment { get; set; } = new();
    }
}
