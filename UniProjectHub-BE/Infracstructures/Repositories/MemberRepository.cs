﻿using Application.Commons;
using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Member> _dbSet;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Member>();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async System.Threading.Tasks.Task AddAsync(Member member)
        {
            await _dbSet.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public void AddAttach(Member model)
        {
            _dbSet.Attach(model);
            _context.SaveChanges();
        }

        public void AddEntry(Member model)
        {
            _context.Entry(model).State = EntityState.Added;
            _context.SaveChanges();
        }

        public async System.Threading.Tasks.Task AddRangeAsync(List<Member> models)
        {
            await _dbSet.AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        public async Task<Member> CloneAsync(Member model)
        {
            var clonedMember = new Member
            {
                ProjectId = model.ProjectId,
                MenberId = model.MenberId,
                IsOwner = model.IsOwner,
                Role = model.Role,
                JoinTime = model.JoinTime,
                LeftTime = model.LeftTime,
                User = model.User,
                Project = model.Project
            };

            await _dbSet.AddAsync(clonedMember);
            await _context.SaveChangesAsync();

            return clonedMember;
        }

        public void Delete(object id)
        {
            var memberToDelete = _dbSet.Find(id);
            if (memberToDelete != null)
            {
                _dbSet.Remove(memberToDelete);
                _context.SaveChanges();
            }
        }

        public void Delete(Member entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public async System.Threading.Tasks.Task DeleteAsync(Member member)
        {
            _dbSet.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<Member>> GetAllAsync(Func<IQueryable<Member>, IIncludableQueryable<Member, object>> include = null)
        {
            IQueryable<Member> query = _dbSet;
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<List<Member>> GetAllAsync(Func<IQueryable<Member>, IQueryable<Member>> filter = null, Func<IQueryable<Member>, IIncludableQueryable<Member, object>> include = null)
        {
            IQueryable<Member> query = _dbSet;
            if (filter != null)
            {
                query = filter(query);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public Member GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<IEnumerable<Member>> GetMembersByProjectIdAsync(int projectId)
        {
            return await _dbSet.Where(m => m.ProjectId == projectId).ToListAsync();
        }

        public void Insert(Member entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task<Pagination<Member>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            var totalItems = await _dbSet.CountAsync();
            var items = await _dbSet.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<Member>(items, totalItems, pageIndex, pageSize);
        }

        public void Update(Member member)
        {
            _dbSet.Update(member);
            _context.SaveChanges();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(List<Member> models)
        {
            _dbSet.UpdateRange(models);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<int>> GetProjectIdsByUserOwnerAsync(string userId)
        {
            return await _dbSet
                .Where(m => m.MenberId == userId && m.IsOwner)
                .Select(m => m.ProjectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetProjectIdsByUserNotOwnerAsync(string userId)
        {
            return await _dbSet
                .Where(m => m.MenberId == userId && !m.IsOwner)
                .Select(m => m.ProjectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetProjectIdsByUserAsync(string userId)
        {
            return await _dbSet
                .Where(m => m.MenberId == userId)
                .Select(m => m.ProjectId)
                .ToListAsync();
        }
        public async Task<Member> GetMemberByIdAsync(string userId)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.MenberId == userId);
        }
        Task<List<Member>> IGenericRepository<Member>.GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public System.Threading.Tasks.Task UpdateAsync(GroupChat groupChat)
        {
            throw new NotImplementedException();
        }
    }
}
