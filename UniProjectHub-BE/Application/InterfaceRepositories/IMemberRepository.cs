using Domain.Models;

namespace Application.InterfaceRepositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member> GetByIdAsync(int id);
        Task<IEnumerable<Member>> GetAllAsync();
        Task<IEnumerable<Member>> GetMembersByProjectIdAsync(int projectId);
        Task<IEnumerable<int>> GetProjectIdsByUserOwnerAsync(string userId);
        Task<IEnumerable<int>> GetProjectIdsByUserNotOwnerAsync(string userId);
        Task<IEnumerable<int>> GetProjectIdsByUserAsync(string userId);
        System.Threading.Tasks.Task AddAsync(Member member);
        System.Threading.Tasks.Task UpdateAsync(Member member);
        System.Threading.Tasks.Task DeleteAsync(Member member);
    }
}
