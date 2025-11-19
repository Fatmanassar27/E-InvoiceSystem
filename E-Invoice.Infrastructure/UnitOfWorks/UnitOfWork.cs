using System.Collections.Concurrent;
using E_Invoice.Application.Interfaces;
using E_Invoice.Infrastructure.Data;
using E_Invoice.Infrastructure.Repositories;

namespace E_Invoice.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        #region implementation using specific Repositories
        private readonly EInvoiceDbContext DbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(EInvoiceDbContext dbContext)
        {
            DbContext = dbContext;
            _repositories = new ConcurrentDictionary<string, object>();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : class
            where TKey : IEquatable<TKey>
        {
            return  (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(DbContext));
        }
        #endregion

        private IAcceptedDocumentRepository _acceptedDocumentRepository;

        public IAcceptedDocumentRepository AcceptedDocumentRepository => _acceptedDocumentRepository ??= new AcceptedDocumentRepository(DbContext);

        public async ValueTask DisposeAsync() => await DbContext.DisposeAsync();
        public async Task<int> SaveChangesAsync() => await DbContext.SaveChangesAsync();
    }
}
