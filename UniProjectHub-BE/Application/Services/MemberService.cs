using Application.InterfacesService;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
   
        public class MemberService : IMemberService
        {
            private readonly IMemberRepository _memberRepository;

            public MemberService(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            public async Task<IEnumerable<Member>> GetMembersAsync()
            {
                return await _memberRepository.GetMembersAsync();
            }

            public async Task<Member> GetMemberByIdAsync(int id)
            {
                return await _memberRepository.GetMemberByIdAsync(id);
            }

            public async Task<Member> CreateMemberAsync(Member member)
            {
                return await _memberRepository.CreateMemberAsync(member);
            }

            public async Task<Member> UpdateMemberAsync(Member member)
            {
                return await _memberRepository.UpdateMemberAsync(member);
            }

            /*public async Task DeleteMemberAsync(int id)
            {
                await _memberRepository.DeleteMemberAsync(id);
            }*/
        }
    }