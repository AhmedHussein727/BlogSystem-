using Blog.Shared.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Abstraction
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetStatisticsAsync();
    }
}
