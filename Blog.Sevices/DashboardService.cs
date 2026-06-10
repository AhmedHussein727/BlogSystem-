using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Services.Abstraction;
using Blog.Shared.DTOs.Dashboard;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public DashboardService(
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<DashboardDto> GetStatisticsAsync()
        {
            var posts =
                await _unitOfWork
                .GetRepository<BlogPost, int>()
                .CountAsync();

            var comments =
                await _unitOfWork
                .GetRepository<Comment, int>()
                .CountAsync();

            var categories =
                await _unitOfWork
                .GetRepository<Category, int>()
                .CountAsync();

            var users =
                _userManager.Users.Count();

            var recentPosts =
            (await _unitOfWork
            .GetRepository<BlogPost, int>()
            .GetAllAsync())
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .Select(x => x.Title)
            .ToList();

            var recentCategories =
                (await _unitOfWork
                    .GetRepository<Category, int>()
                    .GetAllAsync())
                .Take(5)
                .Select(x => x.Name)
                .ToList();



            return new DashboardDto
            {
                TotalPosts = posts,
                TotalComments = comments,
                TotalCategories = categories,
                TotalUsers = users,

                RecentPosts = recentPosts,
                RecentCategories = recentCategories
            };
        }

    }
}
