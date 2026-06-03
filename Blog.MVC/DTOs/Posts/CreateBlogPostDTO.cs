using Blog.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.MVC.DTOs.Posts
{
    public class CreateBlogPostDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
