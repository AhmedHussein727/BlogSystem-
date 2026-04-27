using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;

        public int PostId { get; set; }= default!;

        public int AuthorId { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        #region Nav Properties
        public AppUser Author { get; set; } = default!;
        public BlogPost Post { get; set; } = default!;
        #endregion
    }
}
