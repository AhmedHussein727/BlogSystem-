using Blog.Domain.Entities;
using Blog.Services.Abstraction;
using Blog.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace Blog.Sevices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<AuthResponseDto<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user =await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null) return new AuthResponseDto<UserDTO>
            {
                IsSuccess = false,
                Message = $" Invalid Email Or Password"
            };
            var ispasswordValid = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password,false);
            if (!ispasswordValid.Succeeded)
                return new AuthResponseDto<UserDTO>
                {
                    IsSuccess = false,
                    Message = $" Invalid Email Or Password"
                };
            var token = await CreateTokenAsync(user);
            var userDto = new UserDTO
            (
                user.Id,
                 user.Email!,
                 user.Name,
                 token
            );
            return new AuthResponseDto<UserDTO>
            {
                IsSuccess=true,
                Message="Login Successful",
                Data= userDto
            };

        }

        public async Task<AuthResponseDto<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new AppUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.DisplayName,
                Name =registerDTO.DisplayName,
                PhoneNumber=registerDTO.PhoneNumber,
            };
            var result=await _userManager.CreateAsync(user,registerDTO.Password);
            

            if (!result.Succeeded)
            {
                return new AuthResponseDto<UserDTO>
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)),
                };
            }

            
            await _userManager.AddToRoleAsync(user, "Reader");

            if (!result.Succeeded)
                return new AuthResponseDto<UserDTO>
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)),
                };
            var token = await CreateTokenAsync(user);

            var userDto= new UserDTO(user.Id, user.Email, user.Name, token);

            return new AuthResponseDto<UserDTO>
            {
                IsSuccess = true,
                Message = "User registered successfully",
                Data = userDto
            };
        }

        public async Task<AuthResponseDto<string>> AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return new AuthResponseDto<string>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                return new AuthResponseDto<string>
                {
                    IsSuccess = false,
                    Message = "Role does not exist"
                };
            }

            if (await _userManager.IsInRoleAsync(user, role))
            {
                return new AuthResponseDto<string>
                {
                    IsSuccess = false,
                    Message = "User already has this role"
                };
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

                 if (currentRoles.Any())
                     await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result=await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                return new AuthResponseDto<string>
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            return new AuthResponseDto<string>
            {
                IsSuccess = true,
                Message = "Role assigned successfully",
                Data = "Done"
            };
        }

        private async Task<string>CreateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.Name!),
            };

            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var secretKey = _configuration["JWTOptions:SecretKey"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<IEnumerable<UserWithRoleDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserWithRoleDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserWithRoleDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email!,
                    Role = roles.FirstOrDefault() ?? "Reader"
                });
            }

            return result;
        }


    }
}
