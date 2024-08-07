using Application.ViewModels.BlogModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IBlogService
    {
        Task<BlogModelView> CreateBlogAsync(BlogCreateModel blogCreateModel);
        Task<BlogModelView> GetBlogAsync(int id);
        Task<IEnumerable<BlogModelView>> GetBlogsAsync();
        System.Threading.Tasks.Task UpdateBlogAsync(BlogUpdateModel blogUpdateModel);
        System.Threading.Tasks.Task DeleteBlogAsync(int id);
        Task<IEnumerable<BlogModelView>> GetBlogsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<BlogModelView>> GetBlogsByOwnerIdAsync(string userId);
    }
}
