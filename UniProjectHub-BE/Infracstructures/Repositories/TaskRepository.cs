﻿using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class TaskRepository : GenericRepository<Domain.Models.Task>, ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Models.Task>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.Set<Domain.Models.Task>()
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
