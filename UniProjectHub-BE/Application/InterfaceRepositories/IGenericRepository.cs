﻿using Application.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        
        TModel GetByID(object id);
        void Insert(TModel entity);
        void Delete(object id);
        void Delete(TModel entityToDelete);
        Task<TModel> CloneAsync(TModel model);
        Task<List<TModel>> GetAllAsync();
        Task<List<TModel>> GetAllAsync(Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null);
        Task<List<TModel>> GetAllAsync(Func<IQueryable<TModel>, IQueryable<TModel>>? filter = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null);
        Task<TModel?> GetByIdAsync(int id);

        Task AddAsync(TModel model);

        void AddAttach(TModel model);
        void AddEntry(TModel model);
        void Update(TModel model);

        void UpdateRange(List<TModel> models);

        Task AddRangeAsync(List<TModel> models);

        // Add paging method to generic interface 
        Task<Pagination<TModel>> ToPaginationAsync(int pageIndex = 0, int pageSize = 10);
    }
}