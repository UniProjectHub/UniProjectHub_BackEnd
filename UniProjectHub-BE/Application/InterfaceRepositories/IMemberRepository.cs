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
        System.Threading.Tasks.Task AddAsync(Member member);
        System.Threading.Tasks.Task Update(Member member);
        System.Threading.Tasks.Task DeleteAsync(Member member);
    }
}
