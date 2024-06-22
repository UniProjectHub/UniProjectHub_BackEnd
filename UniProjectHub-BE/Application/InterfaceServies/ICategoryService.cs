using Application.ViewModels.CategoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel categoryViewModel);
        Task<CategoryViewModel> GetCategoryAsync(int id);
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
        System.Threading.Tasks.Task UpdateCategoryAsync(CategoryViewModel categoryViewModel);
        System.Threading.Tasks.Task DeleteCategoryAsync(int id);
    }
}
