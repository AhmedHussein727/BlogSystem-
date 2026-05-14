using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.IdentityDTOs
{
    public record UserDTO(string Id, string Email, string Name, string Token);
}
