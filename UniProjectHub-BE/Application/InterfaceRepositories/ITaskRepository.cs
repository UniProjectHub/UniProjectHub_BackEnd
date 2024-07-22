using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface ITaskRepository: IGenericRepository<Domain.Models.Task>
    {
 
        Task<IEnumerable<Domain.Models.Task>> GetTasksByProjectIdAsync(int projectId);
    }
}
