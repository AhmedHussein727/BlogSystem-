using Blog.Domain.Interfaces;
using Blog.Presistance.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presistance.Rebositories
{
    public class GenericRebository<TEntity, TKey> : IGenericRebository<TEntity, TKey> where TEntity : class
    {
        private readonly BlogDbContext _DbContext;

        public GenericRebository(BlogDbContext blogDbContext)
        {
            _DbContext = blogDbContext;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _DbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _DbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAllAsync(
                 Expression<Func<TEntity, bool>>? condition,
                 List<Expression<Func<TEntity, object>>>? includes)
        {
            IQueryable<TEntity> query = _DbContext.Set<TEntity>();

            // filter
            if (condition is not null)
            {
                query = query.Where(condition);
            }

            // includes
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query ;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _DbContext.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _DbContext.Set<TEntity>().Update(entity);
        }
    }
}
