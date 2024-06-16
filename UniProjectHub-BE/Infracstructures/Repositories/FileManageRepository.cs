using Application.Commons;
using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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


        public async Task<IEnumerable<File>> GetFileByTaskIdAsync(int taskId)
        {
            return await dbSet
                .Where(file => file.TaskId == taskId)
                .ToListAsync();
        }

        public async Task<IEnumerable<File>> GetFileByUserIdAsync(string userId)
        {
            return await dbSet
                .Where(file => file.UserId == userId)
                .ToListAsync();
        }

        public System.Threading.Tasks.Task UpdateAsync(GroupChat groupChat)
        {
            throw new NotImplementedException();
        }
    }
}
