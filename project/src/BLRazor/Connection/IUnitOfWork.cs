using Data.Repository;

namespace BLRazor.Connection;

public interface IUnitOfWork
{
    IRepositoryBase<TEntity> RepositoryBase<TEntity>() where TEntity :  class;
    Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction();
    Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> TransactionAsync();
    void Commit();
    Task CommitAsync();
}