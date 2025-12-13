using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Persistance.Context;
using Persistance.Repositories;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        readonly ApplicationDbcontext _dbContext;
        IDbContextTransaction? _transaction;
        IProductRepository _productRepository;

        public UnitOfWork(ApplicationDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductRepository Products
        {
            get
            {
                _productRepository ??= new ProductRepository(_dbContext);
                return _productRepository;
            }
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
            {
                throw new InvalidOperationException("Transaction has not been started");
            }

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await RollBackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollBackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
                return;

            try
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
