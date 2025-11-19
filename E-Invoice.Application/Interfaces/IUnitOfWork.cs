using Microsoft.EntityFrameworkCore;

namespace E_Invoice.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        #region implementation using specific Repositories

        IGenericRepository<TEntity, TKey> GetRepository <TEntity, TKey>()
            where TEntity : class
            where TKey : IEquatable<TKey>;

        #endregion

        IAcceptedDocumentRepository AcceptedDocumentRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
