using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Abstraction
{
    public interface ICatService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
