using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> GetAllCommentsByBlogIdAsync(int blogId)
        {
            return await _context.Set<Comment>()
               .Where(c => c.BlogId == blogId)
               .OrderByDescending(c => c.CreatedAt)
               .ToListAsync();
        }
    }
}
