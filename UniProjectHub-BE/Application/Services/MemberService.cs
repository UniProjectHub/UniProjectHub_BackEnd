using Application.InterfaceRepositories;
 using Application.InterfaceServies;
using Application.Validators;
using Application.ViewModels.MemberViewModel;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Import System.ComponentModel.DataAnnotations.ValidationResult
using System.Linq;
using System.Threading.Tasks;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<MemberViewModel> _validator;
    private readonly IValidator<CreateMemberViewModel> _createMemberValidator;

    public MemberService(IMemberRepository memberRepository, IMapper mapper, IValidator<MemberViewModel> validator, IValidator<CreateMemberViewModel> createMemberValidator)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
        _validator = validator;
        _createMemberValidator = createMemberValidator;
    }

    public async Task<Member> GetMemberByIdAsync(int id)
    {
        return await _memberRepository.GetByIdAsync(id);
    }

    public async Task<FluentValidation.Results.ValidationResult> ValidateMemberAsync(MemberViewModel memberViewModel)
    {
        return await _validator.ValidateAsync(memberViewModel);
    }

    public async Task<FluentValidation.Results.ValidationResult> ValidateCreateMemberAsync(CreateMemberViewModel createMemberView)
    {
        return await _createMemberValidator.ValidateAsync(createMemberView);
    }

    public async Task<Member> CreateMemberAsync(CreateMemberViewModel createMemberView)
    {
        var validationResult = await ValidateCreateMemberAsync(createMemberView);

        if (!validationResult.IsValid)
        {
            var validationMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new FluentValidation.ValidationException(validationMessage);
        }

        // Assuming the member object should be created from CreateMemberViewModel
        var member = _mapper.Map<Member>(createMemberView);
        member.JoinTime = DateTime.UtcNow;  // Set JoinTime if not provided
        await _memberRepository.AddAsync(member);
        return member;
    }


    public async Task<Member> UpdateMemberAsync(MemberViewModel memberViewModel, int id)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null)
            throw new KeyNotFoundException("Member not found");

        var validationResult = await ValidateMemberAsync(memberViewModel);
        if (!validationResult.IsValid)
        {
            var validationMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new FluentValidation.ValidationException(validationMessage);
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

    public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MemberViewModel>>(members);
    }

    public async System.Threading.Tasks.Task DeleteMemberAsync(int id)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null)
            throw new KeyNotFoundException("Member not found");

        await _memberRepository.DeleteAsync(member);
    }
}
