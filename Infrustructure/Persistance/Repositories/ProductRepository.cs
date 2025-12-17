using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repositories
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IReadOnlyList<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(p =>  p.IsActive && !p.IsDeleted)
                .OrderBy(p => p.Name.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(sku))
                return null;

            return await DbSet
                .FirstOrDefaultAsync(p => p.Sku == sku, cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrEmpty(searchTerm))
                return new List<Product>();

            return await DbSet
                .Where(p => EF.Functions.Like(p.Name.Value, $"%{searchTerm}%"))
                .OrderBy(p => p.Name.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AnyAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(p => p.IsDeleted)
                .OrderBy(p => p.Name.Value)
                .ToListAsync(cancellationToken);    
        }
    }
}
