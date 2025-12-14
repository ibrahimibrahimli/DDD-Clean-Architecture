using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System.Data.Entity;


namespace Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbcontext _context;
        protected readonly DbSet<T> DbSet;

        public Repository(ApplicationDbcontext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbSet 
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .ToListAsync(cancellationToken);    
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.UpdateAsync(entity, cancellationToken);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.RemoveAsync(entity, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
