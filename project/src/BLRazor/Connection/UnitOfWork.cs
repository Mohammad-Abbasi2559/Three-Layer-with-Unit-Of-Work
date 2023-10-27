using Data;
using Data.Repository;

namespace BLRazor.Connection;

public class UnitOfWork : IUnitOfWork
{
    private readonly ConsoleContext context;

    public UnitOfWork() => context = new();

    public IRepositoryBase<TEntity> RepositoryBase<TEntity>() where TEntity : class => new RepositoryBase<TEntity>(context);

    public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction() => context.Database.BeginTransaction();

    public async Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> TransactionAsync() => await context.Database.BeginTransactionAsync();

    public void Commit() => context.SaveChanges();

    public async Task CommitAsync() => await context.SaveChangesAsync();
}