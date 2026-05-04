using Blog.Domain.Interfaces;
using Blog.Presistance.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presistance.Rebositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];
        public UnitOfWork(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public IGenericRebository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : class
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var respository))
            {
                return (IGenericRebository<TEntity, TKey>)respository;
            }

            var newRepo = new GenericRebository<TEntity, TKey>(_dbContext);

            _repositories[entityType] = newRepo;

            return newRepo;
        }
    
        
    }
}
