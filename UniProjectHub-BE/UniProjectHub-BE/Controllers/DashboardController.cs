using Application.InterfaceServies;
using Microsoft.AspNetCore.Mvc;

namespace UniProjectHub_BE.Controllers
{
     
        [ApiController]
        [Route("api/[controller]")]
        public class DashboardController : ControllerBase
        {
            private readonly IDashboardService _dashboardService;

            public DashboardController(IDashboardService dashboardService)
            {
                _dashboardService = dashboardService;
            }

            [HttpGet("summary")]
            public async Task<IActionResult> GetDashboardSummary()
            {
                var summary = await _dashboardService.GetDashboardSummaryAsync();
                return Ok(summary);
            }
        }
    
}