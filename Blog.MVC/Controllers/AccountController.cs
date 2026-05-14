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
    }
}
