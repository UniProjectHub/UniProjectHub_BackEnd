﻿using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Application.ViewModels.MemberViewModel;
using Application.ViewModels.ProjectViewModel;
using AutoMapper;
using Domain.Models;
using Infracstructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.ViewModels.ProjectViewModel.ProjectViewModel;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Create
        public async Task<ProjectViewModel> CreateProjectAsync(CreateProjectRequest request, string ownerId)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                NameLeader = request.NameLeader,
                TypeOfSpace = request.TypeOfSpace,
                Status = request.Status,
                IsGroup = request.IsGroup,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ProjectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();
            var menber = new Member
            {
                MenberId = ownerId,
                IsOwner = true,
                ProjectId = project.Id,
                Role = 1,
                JoinTime = DateTime.UtcNow

            };
            

            await _unitOfWork.MemberRepository.AddAsync(menber);
            await _unitOfWork.SaveChangesAsync();

            return new ProjectViewModel
            {
                Name = project.Name,
                Description = project.Description,
                NameLeader = project.NameLeader,
                TypeOfSpace = project.TypeOfSpace,
                Status = project.Status,
                IsGroup = project.IsGroup,
                CreatedAt = project.CreatedAt
            };
        }

        // Read
        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
            return projects.Select(p => new ProjectViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                NameLeader = p.NameLeader,
                TypeOfSpace = p.TypeOfSpace,
                Status = p.Status,
                IsGroup = p.IsGroup,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<ProjectViewModel> GetProjectByIdAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return null;

            return new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                NameLeader = project.NameLeader,
                TypeOfSpace = project.TypeOfSpace,
                Status = project.Status,
                IsGroup = project.IsGroup,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<IEnumerable<ProjectViewModel>> GetProjectsByUserOwnerAsync(string userId)
        {
            var projectIds = await _unitOfWork.MemberRepository.GetProjectIdsByUserOwnerAsync(userId);

            var projects = new List<Project>();

            foreach (var projectId in projectIds)
            {
                var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
                if (project != null)
                {
                    projects.Add(project);
                }
            }

            return projects.Select(p => new ProjectViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                NameLeader = p.NameLeader,
                TypeOfSpace = p.TypeOfSpace,
                Status = p.Status,
                IsGroup = p.IsGroup,
                CreatedAt = p.CreatedAt
            });
        }
        public async Task<IEnumerable<ProjectViewModel>> GetProjectsByUserAsync(string userId)
        {
            var projectIds = await _unitOfWork.MemberRepository.GetProjectIdsByUserAsync(userId);

            var projects = new List<Project>();

            foreach (var projectId in projectIds)
            {
                var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
                if (project != null)
                {
                    projects.Add(project);
                }
            }

            return projects.Select(p => new ProjectViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                NameLeader = p.NameLeader,
                TypeOfSpace = p.TypeOfSpace,
                Status = p.Status,
                IsGroup = p.IsGroup,
                CreatedAt = p.CreatedAt
            });
        }


        // Update
        public async Task<ProjectViewModel> UpdateProjectAsync(int id, UpdateProjectRequest request)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return null;

            project.Name = request.Name;
            project.Description = request.Description;
            project.NameLeader = request.NameLeader;
            project.TypeOfSpace = request.TypeOfSpace;
            project.Status = request.Status;
            project.IsGroup = request.IsGroup;

            _unitOfWork.ProjectRepository.Update(project);
            await _unitOfWork.SaveChangesAsync();

            return new ProjectViewModel
            {
                Name = project.Name,
                Description = project.Description,
                NameLeader = project.NameLeader,
                TypeOfSpace = project.TypeOfSpace,
                Status = project.Status,
                IsGroup = project.IsGroup,
                CreatedAt = project.CreatedAt
            };
        }

        // Delete
        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (project == null)
                return false; // Return false if the project is not found

            _unitOfWork.ProjectRepository.Delete(project);
            var result = await _unitOfWork.SaveChangesAsync() > 0; // Check if any changes were saved

            return result;
        }
    }

}

