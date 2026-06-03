using Blog.MVC.DTOs.Account;
using Blog.MVC.DTOs.Category;
using Blog.MVC.DTOs.Comments;
using Blog.MVC.DTOs.Posts;
using Blog.MVC.ServicesAbstraction;
using Blog.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blog.MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public PostsController(
            IHttpClientFactory factory,
            ITokenService tokenService)
        {
            _httpClient = factory.CreateClient("BlogApi");
            _tokenService = tokenService;
        }
        public async Task<IActionResult> Index(
            string? categoryName,
            string? status,
            int pageIndex = 1)
        {
            var url =
                $"BlogPosts?pageIndex={pageIndex}&pageSize=5";

            if (!string.IsNullOrWhiteSpace(categoryName))
                url += $"&categoryName={categoryName}";

            if (!string.IsNullOrWhiteSpace(status))
                url += $"&status={status}";

            var response = await _httpClient
                .GetFromJsonAsync<
                    PaginationResponse<BlogPostDto>>(url);
            var categories =
    await _httpClient.GetFromJsonAsync<
        List<CategoryDTO>>("Categories");

            var vm = new PostsIndexViewModel
            {
                Posts = response!.Data,
                PageIndex = response.PageIndex,
                PageSize = response.PageSize,
                Count = response.Count,

                CategoryName = categoryName,
                Status = status,

                Categories = categories!
         .Select(c => new SelectListItem
         {
             Value = c.Name,
             Text = c.Name
         })
            };

            return View(vm);
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
            AttachToken();

            var response = await _httpClient.PostAsJsonAsync(
                "Comment",
                vm.CreateComment);

            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                return Content(error);
            }

            return RedirectToAction(
                nameof(Details),
                new { id = vm.CreateComment.PostId });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AttachToken();

            var categories = await _httpClient
                .GetFromJsonAsync<List<CategoryDTO>>(
                    "Categories");

            var vm = new CreatePostViewModel
            {
                Categories = categories!
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
    CreatePostViewModel vm)
        {
            AttachToken();

            var response = await _httpClient.PostAsJsonAsync(
                "BlogPosts",
                vm.Post);

            if (!response.IsSuccessStatusCode)
            {
                var categories = await _httpClient
                    .GetFromJsonAsync<List<CategoryDTO>>(
                        "Categories");

                vm.Categories = categories!
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

                return View(vm);
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



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AttachToken();

            var post = await _httpClient
                .GetFromJsonAsync<BlogPostDto>(
                    $"BlogPosts/{id}");

            if (post is null)
                return NotFound();

            var categories = await _httpClient
                .GetFromJsonAsync<List<CategoryDTO>>(
                    "Categories");

            var vm = new EditPostViewModel
            {
                Id = post.Id,

                Post = new UpdatePostDto
                {
                    Title = post.Title,
                    Content = post.Content,
                    CategoryId = post.CategoryId,
                    Status = post.Status
                },

                Categories = categories!
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(
            EditPostViewModel vm)
        {
            AttachToken();

            var response = await _httpClient.PutAsJsonAsync(
                $"BlogPosts/{vm.Id}",
                vm.Post);

            if (!response.IsSuccessStatusCode)
            {
                var categories = await _httpClient
                    .GetFromJsonAsync<List<CategoryDTO>>(
                        "Categories");

                vm.Categories = categories!
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

                var error = await response
                    .Content
                    .ReadAsStringAsync();

                return Content(error);
            }

            return RedirectToAction(
                nameof(Details),
                new { id = vm.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AttachToken();

            var response = await _httpClient.DeleteAsync(
                $"BlogPosts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error =
                    await response.Content.ReadAsStringAsync();

                return Content(
                    $"Delete Failed\n\n{error}");
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId,int PostId)
        {
            AttachToken();
            var response=await _httpClient.DeleteAsync($"Comment/{commentId}");
            if(!response.IsSuccessStatusCode)
            {
                var error=await response.Content.ReadAsStringAsync();
                return Content($"Delete Comment Failed\n\n{error}");

            }
            return RedirectToAction(
                 nameof(Details),
                 new { id = PostId });
        }


    }
}
