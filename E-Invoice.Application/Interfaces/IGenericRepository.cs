using System.Linq.Expressions;

namespace E_Invoice.Application.Interfaces
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(
            bool withTracking = false,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetByIdAsync(
            TKey id,
            params Expression<Func<TEntity, object>>[] includes);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
