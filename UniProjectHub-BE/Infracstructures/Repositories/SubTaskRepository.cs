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
    public class SubTaskRepository : GenericRepository<SubTask>, ISubTaskRepository
    {
        private readonly AppDbContext _context;

        public SubTaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SubTask>> GetAllSubTasksByTaskIdAsync(int taskId)
        {
            return await _context.Set<SubTask>()
               .Where(subTask => subTask.TaskId == taskId)
               .ToListAsync();
        }

    }
}
