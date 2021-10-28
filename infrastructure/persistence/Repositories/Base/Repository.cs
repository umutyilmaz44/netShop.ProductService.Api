using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetShop.ProductService.Domain.Common;
using NetShop.ProductService.Infrastructure.Persistence.Content;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Interfaces.Repository.Extensions;

namespace NetShop.ProductService.Infrastructure.Persistence.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        DbSet<T> dbSet;
        private readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<PagedResponse<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> filter = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        Func<IIncludable<T>, IIncludable> includes = null,
                                        int? pageIndex = null, int? pageSize = null)
        {
            IEnumerable<T> dataList;
            IQueryable<T> query = dbContext.Set<T>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = query.IncludeMultiple(includes);
            }

            long totalCount = query.Count();

            int skipCount = 0;
            if (pageSize == null || pageSize <= 0)
                pageSize = (int)totalCount;

            if (pageIndex == null || pageIndex <= 0)
                pageIndex = 1;

            skipCount = (pageIndex.Value - 1) * pageSize.Value;

            if (orderBy != null)
            {
                dataList = await orderBy(query).Skip(skipCount).Take(pageSize.Value).ToListAsync();
            }
            else
            {
                dataList = await query.Skip(skipCount).Take(pageSize.Value).ToListAsync();
            }

            return new PagedResponse<IEnumerable<T>>(dataList, pageIndex.Value, pageSize.Value, totalCount);
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            await dbSet.AddAsync(entity);
            return entity;
        }
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException($"{nameof(AddRangeAsync)} entities must not be null");
            }

            await dbSet.AddRangeAsync(entities);
        }
        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");

            T existingEntity = await this.dbSet.FindAsync(entity.Id);
            if (existingEntity == null)
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity not found in the db!");

            this.dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(DeleteAsync)} entity must not be null");
            }

            T exitingEntity = await this.dbSet.FindAsync(entity.Id);
            if (exitingEntity == null)
                throw new ArgumentNullException($"{nameof(DeleteAsync)} entity not found in the db!");

            dbContext.Set<T>().Remove(exitingEntity);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => this.dbSet.RemoveRange(entities));
        }
    }
}