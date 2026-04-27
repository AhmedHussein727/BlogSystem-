using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }=default!;
        public string Name { get; set; }= default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        #region Nav Properties
        public ICollection<BlogPost>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }

        #endregion


    }
}
