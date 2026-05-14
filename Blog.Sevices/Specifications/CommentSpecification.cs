using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices.Specifications
{
    public class CommentSpecification : BaseSpecifications<Comment, int>
    {
        public CommentSpecification(int postId)
            : base(x => x.PostId == postId)
        {
            AddInclude(x => x.Post);
            AddInclude(x => x.Author);
            AddOrderByDescending(x => x.CreatedAt);
        }
    }
}
