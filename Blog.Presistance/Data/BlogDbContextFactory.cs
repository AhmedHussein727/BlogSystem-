using Blog.Presistance.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presistance.Data
{
    public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=ep-jolly-lab-aijsvh2v.c-4.us-east-1.aws.neon.tech;" +
                "Database=neondb;" +
                "Username=neondb_owner;" +
                "Password=npg_D2VEmG6dqAQX;" +
                "SSL Mode=Require;" +
                "Trust Server Certificate=true"
            );

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}
