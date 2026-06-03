using System.ComponentModel.DataAnnotations;

namespace Blog.MVC.DTOs.Comments
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = default!;

        public int PostId { get; set; }
    }
}
