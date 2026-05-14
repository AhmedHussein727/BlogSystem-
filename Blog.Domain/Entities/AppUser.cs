using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class AppUser : IdentityUser
    {

        public string Name { get; set; }= default!;


        #region Nav Properties
        public ICollection<BlogPost> Posts { get; set; }= new List<BlogPost>();
        public ICollection<Comment> Comments { get; set; }= new List<Comment>();

        #endregion


    }
}
