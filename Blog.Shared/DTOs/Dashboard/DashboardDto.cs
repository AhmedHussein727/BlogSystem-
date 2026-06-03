using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalPosts { get; set; }

        public int TotalComments { get; set; }

        public int TotalCategories { get; set; }

        public int TotalUsers { get; set; }
    }
}
