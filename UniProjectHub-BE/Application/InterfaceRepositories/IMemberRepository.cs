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
        Task<Member> GetByIdAsync(int id);

        Task<IEnumerable<Member>> GetMembersByProjectIdAsync(int projectId);
        Task<IEnumerable<int>> GetProjectIdsByUserOwnerAsync(string userId);
        Task<IEnumerable<int>> GetProjectIdsByUserAsync(string userId);
        System.Threading.Tasks.Task AddAsync(Member member);
        System.Threading.Tasks.Task UpdateAsync(Member member);
        System.Threading.Tasks.Task DeleteAsync(Member member);
    }
}
