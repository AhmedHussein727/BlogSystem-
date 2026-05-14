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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
           ISpecifications<TEntity, TKey> specifications
       )
        {
            var Query = SpecificationEvaluator.CreateQuery(
                _DbContext.Set<TEntity>(),
                specifications
            );

            return await Query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var Query = SpecificationEvaluator.CreateQuery(
                _DbContext.Set<TEntity>(),
                specifications
            );

            return await Query.FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _DbContext.Set<TEntity>().Update(entity);
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator
                .CreateQuery(_DbContext.Set<TEntity>(), specifications) //_dbContext.Products.where(P=>P.BrandId==2)
                .CountAsync();
        }
    }
}
