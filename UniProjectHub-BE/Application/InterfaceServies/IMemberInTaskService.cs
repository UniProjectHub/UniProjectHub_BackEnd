using Application.ViewModels.MemberInTaskViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IMemberInTaskService
    {
        Task<IEnumerable<MemberInTaskViewModel>> GetAllAsync();
        Task<MemberInTaskViewModel> GetByIdAsync(int id);
        Task<IEnumerable<MemberInTaskViewModel>> GetByTaskIdAsync(int taskId);
        Task<MemberInTaskViewModel> CreateAsync(MemberInTaskCreateModel model);
        Task<MemberInTaskViewModel> UpdateAsync(int id, MemberInTaskUpdateModel model);
        Task<bool> DeleteAsync(int id);
    }
}
