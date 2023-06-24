using System.Linq.Expressions;
using Data.Repository;

namespace BLRazor.Connection;

public class ConnectorBase : IConnectorBase{

    private readonly IRepositoryBase _Repository;

    public ConnectorBase(){
        _Repository = new RepositoryBase();
    }

    public void Create<TEntity>(TEntity entity) where TEntity : class{
        _Repository.Create(entity);
    }

    public TEntity FindFirstByCondition<TEntity>(Expression<Func<TEntity, bool>>? condition = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes) where TEntity : class{
        return _Repository.FindFirstByCondition(condition, orderBy, includes);
    }

    public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction() => _Repository.Transaction();

    public void Commit() => _Repository.Commit();    
}
