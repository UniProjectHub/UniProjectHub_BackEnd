using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesService
{
    public interface IGroupChatService
    {
        Task<IEnumerable<GroupChat>> GetGroupChatsAsync();
        Task<GroupChat> GetGroupChatByIdAsync(int id);
        Task<GroupChat> CreateGroupChatAsync(GroupChat groupChat);
        Task<GroupChat> UpdateGroupChatAsync(GroupChat groupChat);
        //Task DeleteGroupChatAsync(int id);
    }
}