using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface IDashboardRepository
    {
        Task<int> GetTotalSubTasksAsync();

        Task<int> GetTotalUsersAsync();
        Task<int> GetTotalProjectsAsync();
        Task<int> GetTotalBlogsAsync();
        Task<int> GetTotalCommentsAsync();
        Task<int> GetTotalFilesAsync();
        Task<int> GetTotalTasksAsync();
        Task<int> GetTotalNotificationsAsync();
        Task<decimal> GetTotalPaymentsAsync();
    }
}
