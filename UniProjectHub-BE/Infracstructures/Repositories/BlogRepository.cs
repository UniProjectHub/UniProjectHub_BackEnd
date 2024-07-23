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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly AppDbContext _context;
 
        public BlogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Blog>> GetBlogsByCategoryIdAsync(int categoryId)
        {
            return await _context.Set<Blog>()
                .Where(b => b.CategoryID == categoryId)
                .ToListAsync();
        }

        
    }
}
