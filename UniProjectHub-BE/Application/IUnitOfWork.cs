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
        IFileManageRepository FileManageRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        ISubTaskRepository SubTaskRepository { get; }
        IMemberInTaskRepository MemberInTaskRepository { get; }
        IBlogRepository BlogRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task<int> SaveChangesAsync();
    }
}