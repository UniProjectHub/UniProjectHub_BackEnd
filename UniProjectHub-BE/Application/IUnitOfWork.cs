using Application.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures
{
    public interface IUnitOfWork 
    {
        IProjectRepository ProjectRepository { get; }
        IMemberRepository MemberRepository { get; }
        IGroupChatRepository GroupChatRepository { get; }
        ITaskRepository TaskRepository { get; } 
        Task<int> SaveChangesAsync();
    }
}