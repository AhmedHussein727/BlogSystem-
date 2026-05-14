using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Entities.Enums;
using Blog.Domain.Interfaces;
using Blog.Services.Abstraction;
using Blog.Sevices.Specifications;
using Blog.Shared.DTOs;
using Blog.Shared.DTOs.PostsDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices
{
    public class BlogPostsService : IBlogPostsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogPostsService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BlogPostDto?> CreateBlogPostAsync(CreateBlogPostDto createDto, string userId)
        {
            // validate category
            var category = await _unitOfWork
                .GetRepository<Category, int>()
                .GetByIdAsync(createDto.CategoryId);

            if (category is null)
                return null;

            var post = _mapper.Map<BlogPost>(createDto);
            post.AuthorId = userId;

            await _unitOfWork
                .GetRepository<BlogPost, int>()
                .AddAsync(post);

            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                return null;

            return _mapper.Map<BlogPostDto>(post);
        }

        public async Task<bool> DeleteBlogPostAsync(int id)
        {
            var post =await _unitOfWork.GetRepository<BlogPost, int>().GetByIdAsync(id);
            if (post is null) return false;

            _unitOfWork.GetRepository<BlogPost, int>().Delete(post);

            return await _unitOfWork.SaveChangesAsync() > 0;

        }

        public async Task<PaginationResponse<BlogPostDto>> GetAllPostsAsync(string? categoryName, Status? status, PaginationParametersDTO paginationParams)
        {
            var spec = new BlogPostSpecification(
        categoryName,
        status,
        paginationParams.PageIndex,
        paginationParams.PageSize
    );

            // Spec للـ Count بدون Pagination
            var countSpec = new BlogPostSpecification(categoryName, status);

            var repo = _unitOfWork.GetRepository<BlogPost, int>();

            var posts = await repo.GetAllAsync(spec);
            var count = await repo.CountAsync(countSpec);

            var mapped = _mapper.Map<IReadOnlyList<BlogPostDto>>(posts);

            return new PaginationResponse<BlogPostDto>(
                paginationParams.PageIndex,
                paginationParams.PageSize,
                count,
                mapped
            );




        }

        public async Task<BlogPostDto?> GetPostByIdAsync(int id)
        {
            var post = await _unitOfWork
                .GetRepository<BlogPost, int>()
                .GetByIdAsync(new BlogPostSpecification(id));

            if (post is null)
                return null;

            return _mapper.Map<BlogPostDto>(post);
        }

        public async Task<BlogPostDto?> UpdateBlogPost(UpdatePostDto updatePostDto, int PostId)
        {
            var post =await _unitOfWork.GetRepository<BlogPost, int>().GetByIdAsync(PostId);
            if (post is null) return null;
            _mapper.Map(updatePostDto, post);

            _unitOfWork.GetRepository<BlogPost, int>().Update(post);
            var res=  await _unitOfWork.SaveChangesAsync();
            if (res <= 0) return null;

            return _mapper.Map<BlogPostDto>(post);
           
        }
    }
}
