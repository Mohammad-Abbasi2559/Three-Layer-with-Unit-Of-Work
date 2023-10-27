using Data;
using Data.Repository;

namespace BLRazor.Connection;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ConsoleContext context;

    private bool disposed = false;

    public UnitOfWork() => context = new();

    ~UnitOfWork() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }

    public IRepositoryBase<TEntity> RepositoryBase<TEntity>() where TEntity : class => new RepositoryBase<TEntity>(context);

    public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction() => context.Database.BeginTransaction();

    public async Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> TransactionAsync() => await context.Database.BeginTransactionAsync();

    public void Commit() => context.SaveChanges();

    public async Task CommitAsync() => await context.SaveChangesAsync();
}