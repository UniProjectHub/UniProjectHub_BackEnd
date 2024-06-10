﻿using Application;
using Application.InterfaceRepositories;
using Infracstructures.Repositories;

namespace Infracstructures
{
    public class UnitOfWork 
    {
        private readonly AppDbContext _context;
        

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            MemberRepository = new MemberRepository(context);
            GroupChatRepository = new GroupChatRepository(context);
            FileRepository = new FileManageRepository(context);
        }

        public IMemberRepository MemberRepository { get;  }
        public IGroupChatRepository GroupChatRepository { get;  }
        public IFileManageRepository FileRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}