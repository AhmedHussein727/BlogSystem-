using Blog.Services.Abstraction;
using Blog.Shared.DTOs.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presentation.Controllers
{
    public class DashboardController : ApiBaseController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DashboardDto>>
            GetDashboard()
        {
            var dashboard =
                await _dashboardService
                    .GetStatisticsAsync();

            return Ok(dashboard);
        }
    }
}
