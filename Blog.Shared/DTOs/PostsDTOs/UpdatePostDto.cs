using Blog.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.PostsDTOs
{
    public class UpdatePostDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? CategoryId { get; set; }
        public Status? Status { get; set; }
    }
}
