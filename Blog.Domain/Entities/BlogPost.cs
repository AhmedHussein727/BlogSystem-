using Blog.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public int AuthorId { get; set; } = default!;
        public int CategoryId { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Status Status { get; set; }

        #region Navigation Properties
        public AppUser Author { get; set; }= default!;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public Category Category { get; set; } = default!;

        public ICollection<Comment>? Comments { get; set; }=new List<Comment>();
        #endregion
    }
}
