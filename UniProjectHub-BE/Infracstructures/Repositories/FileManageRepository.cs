using Application.Commons;
using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Models.File;

namespace Infracstructures.Repositories
{
    public class FileManageRepository : GenericRepository<File>, IFileManageRepository
    {
        public FileManageRepository(AppDbContext context) : base(context) { }


        public Task<IEnumerable<File>> GetFileByTaskIdAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<File>> GetFileByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task UpdateAsync(GroupChat groupChat)
        {
            throw new NotImplementedException();
        }
    }
}
