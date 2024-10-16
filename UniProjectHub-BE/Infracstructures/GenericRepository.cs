﻿using Application.Commons;
using Application.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public AppDbContext context;
        public DbSet<TEntity> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
             Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null, // Optional parameter for pagination (page number)
            int? pageSize = null)  // Optional parameter for pagination (number of records per page)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Implementing pagination
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                // Ensure the pageIndex and pageSize are valid
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10; // Assuming a default pageSize of 10 if an invalid value is passed

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query.ToList();        
        }

        public virtual int GetMaxId(Func<TEntity, int> idSelector)
        {
            var maxId = dbSet.Select(idSelector).DefaultIfEmpty(0).Max();
            return maxId;
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }


        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        
        public virtual async Task AddAsync(TEntity model)
        {
            await dbSet.AddAsync(model);
        }

        public virtual void AddAttach(TEntity model)
        {
            dbSet.Attach(model).State = EntityState.Added;
        }

        public virtual void  AddEntry(TEntity model)
        {
            dbSet.Entry(model).State = EntityState.Added;
        }

        public virtual async Task AddRangeAsync(List<TEntity> models)
        {
            await dbSet.AddRangeAsync(models);
        }

        public virtual async Task<List<TEntity>> GetAllAsync() => await dbSet.ToListAsync();

         
        public virtual async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public void Update(TEntity? model)
        {
            dbSet.Update(model);
        }

        public void UpdateRange(List<TEntity> models)
        {
            dbSet.UpdateRange(models);
        }

        // Implement to pagination method
        public async Task<Pagination<TEntity>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            var totalItems = await dbSet.CountAsync();
            var items = await dbSet.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return new Pagination<TEntity>(items, totalItems, pageIndex, pageSize);
        }



        public async Task<TEntity> CloneAsync(TEntity model)
        {
            dbSet.Entry(model).State = EntityState.Detached;
            var values = dbSet.Entry(model).CurrentValues.Clone().ToObject() as TEntity;
            return values;
        }

        public Task UpdateAsync(Domain.Models.GroupChat groupChat)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = filter(query);
            }

            return await query.ToListAsync();
        }
    }
}
