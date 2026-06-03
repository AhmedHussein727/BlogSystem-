using Blog.MVC.DTOs;
using Blog.MVC.DTOs.Dashboard;
using Blog.MVC.ServicesAbstraction;
using Blog.Shared.DTOs.Dashboard;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blog.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public DashboardController(
            IHttpClientFactory factory,
            ITokenService tokenService)
        {
            _httpClient = factory.CreateClient("BlogApi");
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            AttachToken();

            var dashboard =
                await _httpClient
                .GetFromJsonAsync<DashboardDTO>(
                    "Dashboard");

            return View(dashboard);
        }

        private void AttachToken()
        {
            var token = _tokenService.GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }
        }
    }
}