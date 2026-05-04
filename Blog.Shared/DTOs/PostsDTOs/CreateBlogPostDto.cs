using Blog.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.PostsDTOs
{
    public class CreateBlogPostDto
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
