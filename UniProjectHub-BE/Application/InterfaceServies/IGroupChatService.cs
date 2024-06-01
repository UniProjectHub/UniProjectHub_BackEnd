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
        Task<ValidationResult> ValidateGroupChatAsync(GroupChatViewModel groupChatViewModel);
        Task<GroupChat> CreateGroupChatAsync(GroupChatViewModel groupChatViewModel);
        Task<GroupChat> UpdateGroupChatAsync(GroupChatViewModel groupChatViewModel, int id);
       // Task DeleteGroupChatAsync(int id);
        Task<IEnumerable<GroupChatViewModel>> GetGroupChatsByProjectIdAsync(int projectId);
    }
}