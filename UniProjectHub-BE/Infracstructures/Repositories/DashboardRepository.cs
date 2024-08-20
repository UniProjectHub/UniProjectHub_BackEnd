using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTotalProjectsAsync()
        {
            return await _context.Projects.CountAsync();
        }

        public async Task<int> GetTotalBlogsAsync()
        {
            return await _context.Blogs.CountAsync();
        }

        public async Task<int> GetTotalCommentsAsync()
        {
            return await _context.Comments.CountAsync();
        }

        public async Task<int> GetTotalFilesAsync()
        {
            return await _context.Files.CountAsync();
        }

        public async Task<int> GetTotalTasksAsync()
        {
            return await _context.Tasks.CountAsync();
        }

        public async Task<int> GetTotalSubTasksAsync()
        {
            return await _context.SubTasks.CountAsync();
        }

        public async Task<int> GetTotalNotificationsAsync()
        {
            return await _context.Notifications.CountAsync();
        }

        public async Task<decimal> GetTotalPaymentsAsync()
        {
            return await _context.Payments.SumAsync(p => p.Amount);
        }

        // Implement the GetTotalCountAsync method correctly
        public async Task<int> GetTotalCountAsync()
        {
            int totalUsers = await _context.Users.CountAsync();
            int totalProjects = await _context.Projects.CountAsync();
            int totalBlogs = await _context.Blogs.CountAsync();
            int totalComments = await _context.Comments.CountAsync();
            int totalFiles = await _context.Files.CountAsync();
            int totalTasks = await _context.Tasks.CountAsync();
            int totalSubTasks = await _context.SubTasks.CountAsync();
            int totalNotifications = await _context.Notifications.CountAsync();

            return totalUsers + totalProjects + totalBlogs + totalComments + totalFiles + totalTasks + totalSubTasks + totalNotifications;
        }
    }
}
