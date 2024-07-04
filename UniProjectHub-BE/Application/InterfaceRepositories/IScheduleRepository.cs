using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        void Delete(Schedule schedule);
        Task<List<Schedule>> GetAllSchedulesAsync();

        // Define methods specific to Schedule entity, if needed
        public Task<IEnumerable<Schedule>> GetScheduleByProjectIdAsync(int id);

    }
}