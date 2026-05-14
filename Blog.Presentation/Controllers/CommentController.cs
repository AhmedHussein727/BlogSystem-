using Blog.Services.Abstraction;
using Blog.Shared.DTOs.CommentsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presentation.Controllers
{
    public class CommentController : ApiBaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<GetCommentsDto>>> GetAllComments(int PostId)
        {
            var comments=await _commentService.GetCommentsAsync(PostId);
            return Ok(comments);
        }
        [HttpPost]
        [Authorize]
        //[AllowAnonymous]
        public async Task<ActionResult<GetCommentsDto>> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var comment = await _commentService.CreateCommentAsync(createCommentDto,userId);
            if (comment is null)
                return BadRequest("Invalid PostId");

            return CreatedAtAction(
                nameof(GetAllComments),
                new { postId = comment.PostId },
                comment
            );
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();

            var result = await _commentService.DeleteComment(commentId, userId);

            if (!result)
                return NotFound(); 

            return NoContent();
        }
    }
}
