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
    public class MemberInTaskRepository : GenericRepository<MemberInTask>, IMemberInTaskRepository
    {
        private readonly AppDbContext _context;

        public MemberInTaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MemberInTask>> GetByTaskIdAsync(int taskId)
        {
            return await _context.Set<MemberInTask>()
                .Where(mit => mit.TaskId == taskId)
                .ToListAsync();
        }
    }
}
