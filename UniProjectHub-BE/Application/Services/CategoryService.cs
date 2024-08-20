using Application.InterfaceServies;
using Application.ViewModels;
using Application.ViewModels.CategoryViewModel;
using AutoMapper;
using Domain.Models;
using Infracstructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                CreatedAt = TimeHelper.GetVietnamTime(),
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description,
            };
            
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return categoryViewModel;
        }

        public async Task<CategoryViewModel> GetCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async System.Threading.Tasks.Task UpdateCategoryAsync(CategoryViewModel categoryViewModel)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(categoryViewModel.Id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            _mapper.Map(categoryViewModel, category);
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                _unitOfWork.CategoryRepository.Delete(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }


    }
}
