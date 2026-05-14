

namespace Blog.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRebository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : class;
    }
}
