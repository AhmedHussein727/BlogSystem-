using Blog.MVC.DTOs;
using Blog.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Blog.MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostsController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("BlogApi");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient
                .GetFromJsonAsync<PaginationResponse<BlogPostDto>>("BlogPosts");

            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post=await _httpClient.GetFromJsonAsync<BlogPostDto>($"BlogPosts/{id}");

            if (post is null)
                return NotFound();

            var comments = await _httpClient
                .GetFromJsonAsync<List<GetCommentsDto>>
                ($"Comment/post/{id}");

            var viewModel = new PostDetailsViewModel
            {
                Post = post,
                Comments = comments ?? []
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(
            PostDetailsViewModel vm)
        {
            var dto = vm.CreateComment;
            var response = await _httpClient.PostAsJsonAsync(
                "Comment",
                dto);

            if (!response.IsSuccessStatusCode)
                return BadRequest();

            return RedirectToAction(
                nameof(Details),
                new { id = dto.PostId });
        }
    }
}
