using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Domain.Models;
using AutoMapper;
using Application.ViewModels.GroupChatViewModel;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IGroupChatRepository _groupChatRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GroupChatViewModel> _validator;

        public GroupChatService(IGroupChatRepository groupChatRepository, IMapper mapper, IValidator<GroupChatViewModel> validator)
        {
            _groupChatRepository = groupChatRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<FluentValidation.Results.ValidationResult> ValidateGroupChatAsync(GroupChatViewModel groupChatViewModel)
        {
            return await _validator.ValidateAsync(groupChatViewModel);
        }

        public async Task<GroupChat> CreateGroupChatAsync(GroupChatViewModel groupChatViewModel)
        {
            var validationResult = await ValidateGroupChatAsync(groupChatViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, validationResult.Errors);
            }

            var groupChat = _mapper.Map<GroupChat>(groupChatViewModel);
            await _groupChatRepository.AddAsync(groupChat);
            return groupChat;
        }

        public async Task<GroupChat> UpdateGroupChatAsync(GroupChatViewModel groupChatViewModel, int id)
        {
            var groupChat = await _groupChatRepository.GetByIdAsync(id);
            if (groupChat == null)
                throw new KeyNotFoundException("Group chat not found");

            var validationResult = await ValidateGroupChatAsync(groupChatViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, validationResult.Errors);
            }

            _mapper.Map(groupChatViewModel, groupChat);
            _groupChatRepository.Update(groupChat);
            return groupChat;
        }

        public async Task<IEnumerable<GroupChatViewModel>> GetGroupChatsByProjectIdAsync(int projectId)
        {
            var groupChats = await _groupChatRepository.GetGroupChatsByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<GroupChatViewModel>>(groupChats);
        }

        Task<System.ComponentModel.DataAnnotations.ValidationResult> IGroupChatService.ValidateGroupChatAsync(GroupChatViewModel groupChatViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
