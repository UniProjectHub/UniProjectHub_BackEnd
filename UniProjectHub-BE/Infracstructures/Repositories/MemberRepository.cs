using Domain.Models;
using Infracstructures.InterfacesRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<Member> CreateMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return member;
        }

        /*public async Task DeleteMemberAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }*/
    }
}