using Blog.Shared.DTOs.CommentsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Abstraction
{
    public interface ICommentService
    {
        Task<IEnumerable<GetCommentsDto>> GetCommentsAsync(int postId);

        Task<GetCommentsDto?>CreateCommentAsync(CreateCommentDto createCommentDto, string userId);

         Task<bool> DeleteComment(int CommentId,string userId);
    }
}
