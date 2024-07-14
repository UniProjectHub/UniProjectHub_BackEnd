using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Application.ViewModels.CommentViewModel;
using AutoMapper;
using Domain.Models;
using Infracstructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommentViewModel> CreateCommentAsync(CommentViewModel commentViewModel)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(commentViewModel.BlogId);
            if (blog == null)
                return null;
            var comment = new Comment {
                Description = commentViewModel.Description,
                BlogId = commentViewModel.BlogId,
                CreatedAt = DateTime.Now,
                OwnerId = commentViewModel.OwnerId,
                Status = true
            };
            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();
            return commentViewModel;
        }

        public async Task<CommentViewModel> GetCommentAsync(int id)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(id);
            return _mapper.Map<CommentViewModel>(comment);
        }

        public async Task<IEnumerable<CommentViewModel>> GetCommentsAsync()
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }

        public async System.Threading.Tasks.Task UpdateCommentAsync(CommentViewModel commentViewModel)
        {
            var comment = _mapper.Map<Comment>(commentViewModel);
            _unitOfWork.CommentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteCommentAsync(int id)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(id);
            if (comment != null)
            {
                _unitOfWork.CommentRepository.Delete(comment);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CommentViewModel>> GetAllCommentsByBlogIdAsync(int blogId)
        {
            var comments = await _unitOfWork.CommentRepository.GetAllCommentsByBlogIdAsync(blogId);
            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }
    }
}

