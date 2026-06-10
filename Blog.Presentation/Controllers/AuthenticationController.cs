using Blog.Services.Abstraction;
using Blog.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presentation.Controllers
{
    public class AuthenticationController:ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto<UserDTO>>> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authenticationService.LoginAsync(loginDTO);
            if(!result.IsSuccess) return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto<UserDTO>>> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authenticationService.RegisterAsync(registerDTO);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var result = await _authenticationService.AssignRoleAsync(dto.UserId, dto.Role);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authenticationService.GetAllUsersAsync();
            return Ok(result);
        }
    }
}
