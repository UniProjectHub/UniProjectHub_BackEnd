using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.InterfacesRepository
{
    public interface IMemberRepository
    {
        
            Task<IEnumerable<Member>> GetMembersAsync();
            Task<Member> GetMemberByIdAsync(int id);
            Task<Member> CreateMemberAsync(Member member);
            Task<Member> UpdateMemberAsync(Member member);

/*          Task<Member> DeleteMemberAsync(int id);
*/
    }
    }