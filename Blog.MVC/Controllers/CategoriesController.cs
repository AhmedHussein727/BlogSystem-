using Blog.MVC.DTOs;
using Blog.MVC.DTOs.Category;
using Blog.MVC.ServicesAbstraction;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blog.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public CategoriesController(
            IHttpClientFactory factory,
            ITokenService tokenService)
        {
            _httpClient = factory.CreateClient("BlogApi");
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            var categories =
                await _httpClient
                .GetFromJsonAsync<List<CategoryDTO>>(
                    "Categories");

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(
    CreateCategoryDto dto)
        {
            AttachToken();

            var response =
                await _httpClient.PostAsJsonAsync(
                    "Categories",
                    dto);

            if (!response.IsSuccessStatusCode)
                return View(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category =
                await _httpClient
                .GetFromJsonAsync<CategoryDTO>(
                    $"Categories/{id}");

            if (category is null)
                return NotFound();

            var dto = new UpdateCategoryDto
            {
                Name = category.Name
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
    int id,
    UpdateCategoryDto dto)
        {
            AttachToken();

            var response =
                await _httpClient.PutAsJsonAsync(
                    $"Categories/{id}",
                    dto);

            if (!response.IsSuccessStatusCode)
                return View(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AttachToken();

            var response =
                await _httpClient.DeleteAsync(
                    $"Categories/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content
                    .ReadAsStringAsync();

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
                    new AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }
        }
    }
}