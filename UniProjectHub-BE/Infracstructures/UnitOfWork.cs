﻿using Application;
using Application.Repositories;

namespace Infracstructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            
        }

        

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}