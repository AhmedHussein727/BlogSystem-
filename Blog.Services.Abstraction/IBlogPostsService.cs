using Blog.Domain.Entities.Enums;
using Blog.Shared.DTOs;
using Blog.Shared.DTOs.PostsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Abstraction
{
    public interface IBlogPostsService
    {
        public Task<PaginationResponse<BlogPostDto>> GetAllPostsAsync(string? categoryName,Status? status, PaginationParametersDTO paginationParams);

        Task<BlogPostDto?> GetPostByIdAsync(int id);

        public Task<BlogPostDto?> CreateBlogPostAsync(CreateBlogPostDto createDto);

        public Task<BlogPostDto?> UpdateBlogPost(UpdatePostDto updatePostDto,int PostId);

        public Task<bool> DeleteBlogPostAsync(int id);
    }
}
