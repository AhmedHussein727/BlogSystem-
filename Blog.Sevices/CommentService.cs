using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Services.Abstraction;
using Blog.Shared.DTOs.CommentsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCommentsDto?> CreateCommentAsync(CreateCommentDto dto, string userId)
        {
            var post = await _unitOfWork
                .GetRepository<BlogPost, int>()
                .GetByIdAsync(dto.PostId);

            if (post is null) return null;

            var comment = _mapper.Map<Comment>(dto);

            comment.AuthorId = userId;
            comment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork
                .GetRepository<Comment, int>()
                .AddAsync(comment);

            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0) return null;

            return _mapper.Map<GetCommentsDto>(comment);
        }

        public async Task<bool> DeleteComment(int CommentId, string userId)
        {
            var comment=await _unitOfWork.GetRepository<Comment, int>().GetByIdAsync(CommentId);
            if (comment is null)
                return false;

            if (comment.AuthorId != userId)
                return false;

            _unitOfWork.GetRepository<Comment, int>().Delete(comment);

            if (await _unitOfWork.SaveChangesAsync() <=0) return false;
            return true;


        }

        public async Task<IEnumerable<GetCommentsDto>> GetCommentsAsync(int postId)
        {
            var comments=await _unitOfWork.GetRepository<Comment,int>().GetAllAsync(x=>x.PostId== postId, new List<Expression<Func<Comment, object>>>
            {
                c=>c.Post,
                c=>c.Author
            });

            return _mapper.Map<IEnumerable<GetCommentsDto>>(comments);


        }


    }
}
