using Blog.MVC.ServicesAbstraction;
using Blog.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Blog.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public UsersController(
            IHttpClientFactory factory,
            ITokenService tokenService)
        {
            _httpClient = factory.CreateClient("BlogApi");
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            AttachToken();
            var users = await _httpClient
                .GetFromJsonAsync<List<UserWithRoleDto>>(
                    "Authentication/users");
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(
            string userId, string role)
        {
            AttachToken();
            var response = await _httpClient.PostAsJsonAsync(
                "Authentication/assignRole",
                new { UserId = userId, Role = role });

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return Content(error);
            }

            return RedirectToAction(nameof(Index));
        }

        private void AttachToken()
        {
            var token = _tokenService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}