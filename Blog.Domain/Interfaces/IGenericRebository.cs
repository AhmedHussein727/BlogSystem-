using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces
{
    public interface IGenericRebository<TEntity, Tkey> where TEntity : class
    {
        public IQueryable<TEntity> GetAllAsync(
                 Expression<Func<TEntity, bool>>? condition,
                 List<Expression<Func<TEntity, object>>>? includes);

        Task<TEntity?> GetByIdAsync(Tkey id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
