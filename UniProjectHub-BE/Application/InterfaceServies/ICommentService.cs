using Application.ViewModels.CommentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface ICommentService
    {
        Task<CommentViewModel> CreateCommentAsync(CommentViewModel commentViewModel);
        Task<CommentViewModel> GetCommentAsync(int id);
        Task<IEnumerable<CommentViewModel>> GetCommentsAsync();
        System.Threading.Tasks.Task UpdateCommentAsync(CommentViewModel commentViewModel);
        System.Threading.Tasks.Task DeleteCommentAsync(int id);
        Task<IEnumerable<CommentViewModel>> GetAllCommentsByBlogIdAsync(int blogId);
    }
}
