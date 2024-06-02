using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface IFileManageRepository
    {
        Task<IEnumerable<File>> GetFileByTaskIdAsync(int taskId);
        Task<IEnumerable<File>> GetFileByUserIdAsync(int userId);
    }
}
