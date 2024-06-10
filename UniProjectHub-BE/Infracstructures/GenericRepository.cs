using Application.Commons;
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
    public class GenericRepository<TEntity> where TEntity : class
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

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChangesAsync();
        }

        public virtual async Task AddAsync(TEntity model)
        {
            await dbSet.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public virtual void AddAttach(TEntity model)
        {
            dbSet.Attach(model).State = EntityState.Added;
        }

        public virtual void AddEntry(TEntity model)
        {
            dbSet.Entry(model).State = EntityState.Added;
        } 

        public virtual async Task AddRangeAsync(List<TEntity> models)
        {
            await dbSet.AddRangeAsync(models);
        }

        public virtual async Task<List<TEntity>> GetAllAsync() => await dbSet.ToListAsync();

        /// <summary>
        /// The function return list of TEntity with an include method.
        /// Example for user we want to include the relation Role: 
        /// + GetAllAsync(user => user.Include(x => x.Role));
        /// </summary>
        /// <param name="include"> The linq expression for include relations we want. </param>
        /// <returns> Return the list of TEntity include relations. </returns>
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

        public void UpdateRange(List<TEntity> models)
        {
            dbSet.UpdateRange(models);
        }

        // Implement to pagination method
        public async Task<Pagination<TEntity>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10)
        {
            // get total count of items in the db set
            var itemCount = await dbSet.CountAsync();

            // Create Pagination instance
            // to set data related to paging
            // Calculate and replace pageIndex and pageSize
            // if they are invalid
            var result = new Pagination<TEntity>()
            {
                PageSize = pageSize,
                TotalItemCount = itemCount,
                PageIndex = pageIndex,
            };

            // Take items according to the page size and page index
            // skip items in the previous pages
            // and take next items equal to page size
            var items = await dbSet.Skip(result.PageIndex * result.PageSize)
                .Take(result.PageSize)
                .AsNoTracking()
                .ToListAsync();

            // Assign items to page
            result.Items = items;
            return result;
        }

        public async Task<TEntity> CloneAsync(TEntity model)
        {
            dbSet.Entry(model).State = EntityState.Detached;
            var values = dbSet.Entry(model).CurrentValues.Clone().ToObject() as TEntity;
            return values;
        }

    }
}
