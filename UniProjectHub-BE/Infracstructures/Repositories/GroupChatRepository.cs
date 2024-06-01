using Application.Commons;
using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class GroupChatRepository : GenericRepository<GroupChat>, IGroupChatRepository
    {
        public GroupChatRepository(AppDbContext context) : base(context) { }

        public System.Threading.Tasks.Task AddAsync(GroupChat model)
        {
            throw new NotImplementedException();
        }

        public void AddAttach(GroupChat model)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(GroupChat model)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task AddRangeAsync(List<GroupChat> models)
        {
            throw new NotImplementedException();
        }

        public Task<GroupChat> CloneAsync(GroupChat model)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupChat>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupChat>> GetAllAsync(Func<IQueryable<GroupChat>, IIncludableQueryable<GroupChat, object>>? include = null)
        {
            throw new NotImplementedException();
        }

        public Task<GroupChat?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupChat>> GetGroupChatsByProjectIdAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<GroupChat>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<GroupChat> models)
        {
            throw new NotImplementedException();
        }
    }
}