using Blog.MVC.ServicesAbstraction;

namespace Blog.MVC.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetToken()
        {
            return _httpContextAccessor
                .HttpContext?
                .Request
                .Cookies["AccessToken"];
        }
    }
}
