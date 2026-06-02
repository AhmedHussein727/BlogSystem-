using Blog.MVC.ServicesAbstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.MVC.Services
{
    public class TokenParserService : ITokenParserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenParserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserName()
        {
            var token = _httpContextAccessor
                .HttpContext?
                .Request
                .Cookies["AccessToken"];

            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            return jwt.Claims
                .FirstOrDefault(c =>
                    c.Type == ClaimTypes.Name)
                ?.Value;
        }

        public string? GetRole()
        {
            var token = _httpContextAccessor
                .HttpContext?
                .Request
                .Cookies["AccessToken"];

            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            return jwt.Claims
                .FirstOrDefault(c =>
                    c.Type == ClaimTypes.Role)
                ?.Value;
        }

        public bool IsLoggedIn()
        {
            return _httpContextAccessor
                .HttpContext?
                .Request
                .Cookies
                .ContainsKey("AccessToken") == true;
        }
    }
}