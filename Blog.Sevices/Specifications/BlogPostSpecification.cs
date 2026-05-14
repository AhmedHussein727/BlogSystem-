using Blog.Domain.Entities;
using Blog.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices.Specifications
{
    public class BlogPostSpecification : BaseSpecifications<BlogPost,int>
    {
        public BlogPostSpecification(string? categoryName, Status? status, int pageIndex, int pageSize)
        : base(x =>
            (string.IsNullOrEmpty(categoryName) || x.Category.Name == categoryName) &&
            (!status.HasValue || x.Status == status))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Author);
            AddOrderByDescending(x => x.CreatedAt);
            ApplyPagination(pageSize, pageIndex);
        }
        public BlogPostSpecification(string? categoryName, Status? status)
       : base(x =>
           (string.IsNullOrEmpty(categoryName) || x.Category.Name == categoryName) &&
           (!status.HasValue || x.Status == status))
        {
        }

        public BlogPostSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(x => x.Category);

        }


    }
}
