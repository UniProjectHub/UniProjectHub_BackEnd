using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Models.File;

namespace Application.InterfaceRepositories
{
    public interface IFileManageRepository : IGenericRepository<File>
    {
 
        Task<IEnumerable<File>> GetFileByTaskIdAsync(int taskId);
        Task<IEnumerable<File>> GetFileByUserIdAsync(string userId);
        Task<bool> IsDuplicateFileAsync(int taskId, string fileName);
    }
}
