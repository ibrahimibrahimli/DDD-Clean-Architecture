using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context
{
    public sealed class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbcontext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<BaseEntity>()
           .Select(e => e.Entity)
           .Where(e => e.DomainEvents.Any())
           .SelectMany(e => e.DomainEvents)
           .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            // (burada event dispatcher istifadə edilə bilər)
            // MediatR ilə publish edə biləriy - bumu gələcəkdə əlavə edəcəyiy
            foreach (var entity in ChangeTracker.Entries<BaseEntity>().Select(e => e.Entity))
            {
                entity.ClearDomainEvents();
            }

            return result;
        }
    }
}
