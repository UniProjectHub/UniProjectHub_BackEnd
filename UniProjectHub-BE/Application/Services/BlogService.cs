using Application.Commons;
using Application.InterfaceServies;
using Application.ViewModels;
using Application.ViewModels.BlogModelView;
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
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BlogModelView> CreateBlogAsync(BlogCreateModel blogCreateModel)
        {
            var blog = _mapper.Map<Blog>(blogCreateModel);
            blog.CreatedAt = TimeHelper.GetVietnamTime();
            await _unitOfWork.BlogRepository.AddAsync(blog);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BlogModelView>(blog);
        }

        public async Task<BlogModelView> GetBlogAsync(int id)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return null;
            }
            return _mapper.Map<BlogModelView>(blog);
        }

        public async Task<IEnumerable<BlogModelView>> GetBlogsByCategoryIdAsync(int categoryId)
        {
            var blogs = await _unitOfWork.BlogRepository.GetBlogsByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<BlogModelView>>(blogs);
        }
        public async Task<IEnumerable<BlogModelView>> GetBlogsByOwnerIdAsync(string userId)
        {
            var blogs = await _unitOfWork.BlogRepository.GetBlogsByOwnerIdAsync(userId);
            return _mapper.Map<IEnumerable<BlogModelView>>(blogs);
        }

        public async Task<IEnumerable<BlogModelView>> GetBlogsAsync()
        {
            var blogs = await _unitOfWork.BlogRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BlogModelView>>(blogs);
        }

        public async System.Threading.Tasks.Task UpdateBlogAsync(BlogUpdateModel blogUpdateModel)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(blogUpdateModel.Id);
            if (blog == null)
            {
                return;
            }
            _mapper.Map(blogUpdateModel, blog);
            _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteBlogAsync(int id)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return;
            }
            _unitOfWork.BlogRepository.Delete(blog);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
