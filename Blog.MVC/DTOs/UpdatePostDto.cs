using Blog.Domain.Entities.Enums;

namespace Blog.MVC.DTOs
{
        public class UpdatePostDto
        {
            public string? Title { get; set; }

            public string? Content { get; set; }

            public int? CategoryId { get; set; }

            public Status? Status { get; set; }
        }
}
