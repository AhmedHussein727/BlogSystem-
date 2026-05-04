using Blog.Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Abstraction
{
    public interface IAuthenticationService
    {
        // login  (email,password)=>(token,displayname,email)
        Task<AuthResponseDto<UserDTO>> LoginAsync(LoginDTO loginDTO);

        //register (email , password , username ,phone number)=>((token,username,email))
        Task<AuthResponseDto<UserDTO>> RegisterAsync(RegisterDTO registerDTO);

        Task<AuthResponseDto<string>> AssignRoleAsync(string userId, string role);
    }
}
