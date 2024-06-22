using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<IEnumerable<Member>> GetMembersByProjectIdAsync(int projectId);
        Task<IEnumerable<int>> GetProjectIdsByUserOwnerAsync(string userId);
        Task<IEnumerable<int>> GetProjectIdsByUserAsync(string userId);
    }
}
