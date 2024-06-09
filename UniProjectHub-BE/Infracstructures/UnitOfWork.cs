using Application;
using Application.InterfaceRepositories;
using Domain.Models;
using Infracstructures.Repositories;

namespace Infracstructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context = new AppDbContext();
        private readonly IProjectRepository _projectRepository;

        public UnitOfWork(AppDbContext context, IProjectRepository projectRepository)
        {
            _context = context;
            MemberRepository = new MemberRepository(context);
            GroupChatRepository = new GroupChatRepository(context);
            _projectRepository = projectRepository;
        }

        public IMemberRepository MemberRepository { get;  }
        public IGroupChatRepository GroupChatRepository { get;  }
        public IProjectRepository ProjectRepository => _projectRepository;

       

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}