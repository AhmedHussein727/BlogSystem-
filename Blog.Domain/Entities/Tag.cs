using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
    }
}
