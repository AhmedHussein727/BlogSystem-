using Blog.MVC.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Blog.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("BlogApi");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "Authentication/login",
                dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(dto);
            }

            var result =
                await response.Content.ReadFromJsonAsync<
                    AuthResponseDto<UserDto>>();

            if (result is null || !result.IsSuccess)
            {
                ModelState.AddModelError("", "Login Failed");
                return View(dto);
            }

            Response.Cookies.Append(
                "AccessToken",
                result.Data!.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

            return RedirectToAction("Index", "Posts");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("AccessToken");

            return RedirectToAction(
                "Login",
                "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(
    RegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "Authentication/register",
                dto);

            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                ModelState.AddModelError(
                    "",
                    error);

                return View(dto);
            }

            return RedirectToAction(
                nameof(Login));
        }
    }
}
