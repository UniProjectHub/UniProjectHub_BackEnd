using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface IGroupChatRepository : IGenericRepository<GroupChat>
    {
        Task<IEnumerable<GroupChat>> GetGroupChatsByProjectIdAsync(int projectId);
    }
}
