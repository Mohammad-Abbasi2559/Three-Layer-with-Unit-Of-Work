using Data.Repository;

namespace BLRazor.Connection;

public interface IUnitOfWork
{
    void Dispose();
    IRepositoryBase<TEntity> RepositoryBase<TEntity>() where TEntity :  class;
    Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction();
    Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> TransactionAsync();
    void Commit();
    Task CommitAsync();
}