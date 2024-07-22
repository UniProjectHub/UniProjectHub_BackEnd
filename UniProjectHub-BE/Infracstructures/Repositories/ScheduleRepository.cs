using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Task = System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Set<Schedule>().ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Schedule model)
        {
            await _context.Set<Schedule>().AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddRangeAsync(List<Schedule> models)
        {
            await _context.Set<Schedule>().AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        {
            return await _context.Set<Schedule>().FindAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetScheduleByProjectIdAsync(int id)
        {
            return await _context.Set<Schedule>().Where(s => s.Id == id).ToListAsync();
        }

        public async Task<List<Schedule>> GetAllAsync(Expression<Func<Schedule, bool>>? predicate = null)
        {
            IQueryable<Schedule> query = _context.Set<Schedule>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public void Update(Schedule model)
        {
            _context.Set<Schedule>().Update(model);
            _context.SaveChanges();
        }

        public void Delete(Schedule model)
        {
            _context.Set<Schedule>().Remove(model);
            _context.SaveChanges();
        }
    }
}
