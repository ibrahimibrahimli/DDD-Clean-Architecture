namespace Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products {  get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollBackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
