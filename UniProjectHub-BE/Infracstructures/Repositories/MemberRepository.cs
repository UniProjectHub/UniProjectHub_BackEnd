﻿using Application.Commons;
using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext context) : base(context) { }

        public System.Threading.Tasks.Task AddAsync(Member model)
        {
            throw new NotImplementedException();
        }

        public void AddAttach(Member model)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(Member model)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task AddRangeAsync(List<Member> models)
        {
            throw new NotImplementedException();
        }

        public Task<Member> CloneAsync(Member model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetAllAsync(Func<IQueryable<Member>, IIncludableQueryable<Member, object>>? include = null)
        {
            throw new NotImplementedException();
        }

        public Task<Member?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Member>> GetMembersByProjectIdAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<Member>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task UpdateAsync(GroupChat groupChat)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<Member> models)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<int>> GetProjectIdsByUserOwnerAsync(string userId)
        {
            return await context.Set<Member>()
                .Where(m => m.MenberId == userId && m.IsOwner)
                .Select(m => m.ProjectId)
                .ToListAsync();
        }
        public async Task<IEnumerable<int>> GetProjectIdsByUserAsync(string userId)
        {
            return await context.Set<Member>()
                .Where(m => m.MenberId == userId)
                .Select(m => m.ProjectId)
                .ToListAsync();
        }
    }
}