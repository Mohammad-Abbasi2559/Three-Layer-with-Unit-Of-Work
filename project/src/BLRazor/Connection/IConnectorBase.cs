using System.Linq.Expressions;

namespace BLRazor.Connection;

public interface IConnectorBase{
    void Create<TEntity>(TEntity entity) where TEntity : class;
    TEntity FindFirstByCondition<TEntity>(Expression<Func<TEntity, bool>>? condition = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes) where TEntity : class;
    Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction();
    void Commit();
}
