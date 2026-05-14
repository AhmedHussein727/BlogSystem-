using Blog.Domain.Entities.Enums;
using Blog.Services.Abstraction;
using Blog.Shared.DTOs;
using Blog.Shared.DTOs.PostsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presentation.Controllers
{
    public class BlogPostsController:ApiBaseController
    {
        private readonly IBlogPostsService _blogPostService;


        public BlogPostsController(IBlogPostsService blogPostService)
        {
            _blogPostService = blogPostService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetAllBlogBosts([FromQuery]string? categoryName,[FromQuery]Status? status,
             [FromQuery] PaginationParametersDTO paginationParams)
        {
            var blogPosts =await _blogPostService.GetAllPostsAsync(categoryName,status, paginationParams);

            return Ok(blogPosts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPostById(int id)
        {
            var Post = await _blogPostService.GetPostByIdAsync(id);
            if(Post == null)
                return NotFound();

            return Ok(Post);
        }

        [HttpPost]
        [Authorize(Roles ="Admin,Editor")]
        public async Task<ActionResult<BlogPostDto>> CreateBlogPost(CreateBlogPostDto createBlogPostDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();

            var Post = await _blogPostService
                .CreateBlogPostAsync(createBlogPostDto, userId);
           
            if (Post == null)
                return BadRequest("Invalid data");

            return CreatedAtAction(
                         nameof(GetBlogPostById),   
                         new { id = Post.Id },
                         Post
                                   );
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<ActionResult<BlogPostDto>> UpdateBlogPost(int id, [FromBody] UpdatePostDto updatePostDto)
        {
            var postDto =await _blogPostService.UpdateBlogPost(updatePostDto, id);
            if (postDto == null)
                return NotFound();
            return Ok(postDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var result = await _blogPostService.DeleteBlogPostAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }


}

