using Application.ViewModels.GroupChatViewModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IGroupChatService
    {
        Task<List<GroupChatViewModel>> GetAllGroupChatsAsync();
        Task<GroupChatViewModel> GetGroupChatByIdAsync(int id);
        Task<GroupChatViewModel> UpdateGroupChatAsync(GroupChatViewModel groupChatViewModel, int id);
        System.Threading.Tasks.Task DeleteGroupChatAsync(int id);
    }
}