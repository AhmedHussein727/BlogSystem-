using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.CommentsDtos
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = default!;

        [Range(1, int.MaxValue)]
        public int PostId { get; set; }
    }
}

