using Application.Validators;
using Application.ViewModels.MemberViewModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IMemberService
    {
        Task<Member> GetMemberByIdAsync(int id);

        Task<ValidationResult> ValidateMemberAsync(MemberViewModelValidator memberViewModel);
        Task<Member> CreateMemberAsync(MemberViewModel memberViewModel);
        Task<Member> UpdateMemberAsync(MemberViewModel memberViewModel, int id);
        System.Threading.Tasks.Task DeleteMemberAsync(int id);
        Task<IEnumerable<MemberViewModel>> GetMembersByProjectIdAsync(int projectId);
    }
}