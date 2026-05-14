using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.IdentityDTOs
{
    public record RegisterDTO(
        [EmailAddress] string Email,
        string DisplayName,
        string Password,
        [Phone] string PhoneNumber
    );
}
