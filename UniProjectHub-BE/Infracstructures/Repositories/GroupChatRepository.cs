using Domain.Models;
using Infracstructures.InterfacesRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class GroupChatRepository : IGroupChatRepository
    {
        private readonly AppDbContext _context;

        public GroupChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupChat>> GetGroupChatsAsync()
        {
            return await _context.GroupChats.ToListAsync();
        }

        public async Task<GroupChat> GetGroupChatByIdAsync(int id)
        {
            return await _context.GroupChats.FindAsync(id);
        }

        public async Task<GroupChat> CreateGroupChatAsync(GroupChat groupChat)
        {
            _context.GroupChats.Add(groupChat);
            await _context.SaveChangesAsync();
            return groupChat;
        }

        public async Task<GroupChat> UpdateGroupChatAsync(GroupChat groupChat)
        {
            _context.Entry(groupChat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return groupChat;
        }

       /* public async Task DeleteGroupChatAsync(int id)
        {
            var groupChat = await _context.GroupChats.FindAsync(id);
            if (groupChat != null)
            {
                _context.GroupChats.Remove(groupChat);
                await _context.SaveChangesAsync();
            }
        }*/
    }
}