using Application.Commons;
using Application.InterfaceRepositories;
using Application.ViewModels.GroupChatViewModel;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class GroupChatRepository : GenericRepository<GroupChat>, IGroupChatRepository
    {
       
        public GroupChatRepository(AppDbContext context) : base(context) { }

        public async System.Threading.Tasks.Task AddAsync(GroupChat model) => await dbSet.AddAsync(model);

        public void AddAttach(GroupChat model)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(GroupChat model)
        {
            throw new NotImplementedException();
        }

       

        public async System.Threading.Tasks.Task AddRangeAsync(List<GroupChat> models) => await dbSet.AddRangeAsync(models);

        public async Task<GroupChat> CloneAsync(GroupChat model)
        {
            var clone = (GroupChat)context.Entry(model).CurrentValues.ToObject();
            clone.Id = 0; // Resetting the ID to ensure it's treated as a new entity
            await dbSet.AddAsync(clone);
            return clone;
        }

        public System.Threading.Tasks.Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GroupChat>> GetAllAsync() => await dbSet.ToListAsync();
        
        public async Task<List<GroupChat>> GetAllAsync(Func<IQueryable<GroupChat>, IIncludableQueryable<GroupChat, object>>? include = null)
        {
            IQueryable<GroupChat> query = dbSet;


            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<GroupChat?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public async Task<IEnumerable<GroupChat>> GetGroupChatsByProjectIdAsync(int projectId)
        {
            return await dbSet.Where(gc => gc.ProjectId == projectId).ToListAsync();
        }

        public async Task<Pagination<GroupChat>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            var totalItems = await dbSet.CountAsync();
            var items = await dbSet.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<GroupChat>
            {
                Items = items,
                TotalItemCount = totalItems,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async System.Threading.Tasks.Task UpdateAsync(GroupChat groupChat)
        {
            var existingGroupChat = await context.GroupChats.FindAsync(groupChat.Id);
            if (existingGroupChat == null)
            {
                throw new KeyNotFoundException("GroupChat not found");
            }

            // Update the properties
            existingGroupChat.ProjectId = groupChat.ProjectId;
            existingGroupChat.MemberId = groupChat.MemberId;
            existingGroupChat.Messenger = groupChat.Messenger;
            existingGroupChat.Status = groupChat.Status;

            // Save the changes to the database
            context.GroupChats.Update(existingGroupChat);
            await context.SaveChangesAsync();
        }

        public void UpdateRange(List<GroupChat> models)
        {
            throw new NotImplementedException();
        }
    }
}