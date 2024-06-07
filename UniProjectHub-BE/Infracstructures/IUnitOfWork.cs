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
        IMemberRepository MemberRepository { get; }
        IGroupChatRepository GroupChatRepository { get; }
<<<<<<< HEAD
        IFileManageRepository FileManageRepository { get; }
=======
        IScheduleRepository ScheduleRepository { get; }
>>>>>>> main
        Task<int> SaveChangesAsync();
    }
}