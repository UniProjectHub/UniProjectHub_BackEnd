using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.ViewModels.MemberViewModel;
using FluentValidation;
using FluentValidation.Results;
using Application.Validators;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<MemberViewModel> _validator;
        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        public MemberService(IMemberRepository memberRepository, IMapper mapper, IValidator<MemberViewModel> validator)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<FluentValidation.Results.ValidationResult> ValidateMemberAsync(MemberViewModel memberViewModel)
        {
            return await _validator.ValidateAsync(memberViewModel);
        }

        public async Task<Member> CreateMemberAsync(MemberViewModel memberViewModel)
        {
            var validationResult = await ValidateMemberAsync(memberViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, (IEnumerable<ValidationFailure>)validationResults);
            }

            var member = _mapper.Map<Member>(memberViewModel);
            await _memberRepository.AddAsync(member);
            return member;
        }

        public async Task<Member> UpdateMemberAsync(MemberViewModel memberViewModel, int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                throw new KeyNotFoundException("Member not found");

            var validationResult = await _validator.ValidateAsync(memberViewModel);
            if (!validationResult.IsValid)
            {
                var validationResults = validationResult.Errors
                    .Select(e => new System.ComponentModel.DataAnnotations.ValidationResult(e.ErrorMessage, new[] { e.PropertyName }))
                    .ToList();
                var validationMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(validationMessage, (IEnumerable<ValidationFailure>)validationResults);
            }

            _mapper.Map(memberViewModel, member);
            _memberRepository.Update(member);
            return member;
        }

        public async Task<IEnumerable<MemberViewModel>> GetMembersByProjectIdAsync(int projectId)
        {
            var members = await _memberRepository.GetMembersByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<MemberViewModel>>(members);
        }

        public Task<System.ComponentModel.DataAnnotations.ValidationResult> ValidateMemberAsync(MemberViewModelValidator memberViewModel)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task DeleteMemberAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                throw new KeyNotFoundException("Member not found");

            await _memberRepository.DeleteAsync(member);
        }
    }
}
