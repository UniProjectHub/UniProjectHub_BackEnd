using Application.InterfacesService;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IGroupChatRepository _groupChatRepository;

        public GroupChatService(IGroupChatRepository groupChatRepository)
        {
            _groupChatRepository = groupChatRepository;
        }

        public async Task<IEnumerable<GroupChat>> GetGroupChatsAsync()
        {
            return await _groupChatRepository.GetGroupChatsAsync();
        }

        public async Task<GroupChat> GetGroupChatByIdAsync(int id)
        {
            return await _groupChatRepository.GetGroupChatByIdAsync(id);
        }

        public async Task<GroupChat> CreateGroupChatAsync(GroupChat groupChat)
        {
            return await _groupChatRepository.CreateGroupChatAsync(groupChat);
        }

        public async Task<GroupChat> UpdateGroupChatAsync(GroupChat groupChat)
        {
            return await _groupChatRepository.UpdateGroupChatAsync(groupChat);
        }

/*        public async Task DeleteGroupChatAsync(int id)
        {
            await _groupChatRepository.DeleteGroupChatAsync(id);
        }*/
    }
}