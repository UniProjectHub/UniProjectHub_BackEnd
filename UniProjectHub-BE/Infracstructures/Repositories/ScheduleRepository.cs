using Application.Commons;
using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void AddAttach(Schedule model)
        {
            _context.Set<Schedule>().Attach(model);
            _context.SaveChanges();
        }

        public void AddEntry(Schedule model)
        {
            _context.Entry(model).State = EntityState.Added;
            _context.SaveChanges();
        }

        public async System.Threading.Tasks.Task AddRangeAsync(List<Schedule> models)
        {
            await _context.Set<Schedule>().AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        public async Task<Schedule> CloneAsync(Schedule model)
        {
            var clonedSchedule = new Schedule
            {
                // Implement logic to clone properties of the Schedule model
            };

            await _context.Set<Schedule>().AddAsync(clonedSchedule);
            await _context.SaveChangesAsync();

            return clonedSchedule;
        }

        public async Task<List<Schedule>> GetAllAsync()
        {
            return await _context.Set<Schedule>().ToListAsync();
        }

        public async Task<List<Schedule>> GetAllAsync(Func<IQueryable<Schedule>, IIncludableQueryable<Schedule, object>>? include = null)
        {
            IQueryable<Schedule> query = _context.Set<Schedule>();
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        {
            return await _context.Set<Schedule>().FindAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetScheduleByProjectIdAsync(int id)
        {
            return await _context.Set<Schedule>().Where(s => s.Id == id).ToListAsync();
        }

        public async Task<Pagination<Schedule>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            var totalRecords = await _context.Set<Schedule>().CountAsync();
            var schedules = await _context.Set<Schedule>()
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Pagination<Schedule>(schedules, totalRecords, pageIndex, pageSize);
        }


        public System.Threading.Tasks.Task UpdateAsync(GroupChat groupChat)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<Schedule> models)
        {
            _context.Set<Schedule>().UpdateRange(models);
            _context.SaveChanges();
        }
    }
}
