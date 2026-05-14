using Blog.Domain.Entities;
using Blog.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.PostsDTOs
{
    public class BlogPostDto
    {
        public Status Status { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
    }



}
