using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Application.ViewModels.DashboardSummaryViewModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardSummaryViewModel> GetDashboardSummaryAsync()
        {
            var summary = new DashboardSummaryViewModel
            {
                TotalUsers = await _dashboardRepository.GetTotalUsersAsync(),
                TotalProjects = await _dashboardRepository.GetTotalProjectsAsync(),
                TotalBlogs = await _dashboardRepository.GetTotalBlogsAsync(),
                TotalComments = await _dashboardRepository.GetTotalCommentsAsync(),
                TotalFiles = await _dashboardRepository.GetTotalFilesAsync(),
                TotalTasks = await _dashboardRepository.GetTotalTasksAsync(),
                TotalNotifications = await _dashboardRepository.GetTotalNotificationsAsync(),
                TotalPayments = await _dashboardRepository.GetTotalPaymentsAsync()
            };

            return summary;
        }
    }
}