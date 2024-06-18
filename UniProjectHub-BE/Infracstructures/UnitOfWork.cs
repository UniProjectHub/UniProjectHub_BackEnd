﻿using Application;
using Application.InterfaceRepositories;
using Domain.Models;
using Infracstructures.Repositories;

namespace Infracstructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context = new AppDbContext();
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IMemberInTaskRepository _memberInTaskRepository;

        public UnitOfWork(AppDbContext context, 
            IProjectRepository projectRepository, 
            ITaskRepository taskRepository, 
            ISubTaskRepository subTaskRepository, 
            IMemberInTaskRepository memberInTaskRepository)
        {
            _context = context;
            MemberRepository = new MemberRepository(context);
            GroupChatRepository = new GroupChatRepository(context);
            FileRepository = new FileManageRepository(context);
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
            _memberInTaskRepository = memberInTaskRepository;
        }

        public IMemberRepository MemberRepository { get;  }
        public IGroupChatRepository GroupChatRepository { get;  }
        public IFileManageRepository FileRepository { get; }
        public IProjectRepository ProjectRepository => _projectRepository;
        public ITaskRepository TaskRepository => _taskRepository;
        public ISubTaskRepository SubTaskRepository => _subTaskRepository;
        public IMemberInTaskRepository MemberInTaskRepository => _memberInTaskRepository;

        public IFileManageRepository FileManageRepository => throw new NotImplementedException();

        public IScheduleRepository ScheduleRepository => throw new NotImplementedException();

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}