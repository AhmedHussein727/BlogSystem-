using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.CommentsDtos
{
    public class GetCommentsDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public int PostId { get; set; } = default!;
        public string AuthorName { get; set; }= default!;
    }
}
