using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class RepositoryBase : IRepositoryBase{
    private readonly ConsoleContext db;
 
    public RepositoryBase(){
        db = new ConsoleContext();
    }
    public void Create<TEntity>(TEntity entity) where TEntity : class{
        db.Set<TEntity>().Add(entity);
    }

    public TEntity FindFirstByCondition<TEntity>(Expression<Func<TEntity, bool>> condition = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes) where TEntity : class{
        IQueryable<TEntity> query = db.Set<TEntity>();
        
        foreach (var include in includes){
            query = query.Include(include);
        }

        if (condition != null){
            query = query.Where(condition);
        }

        if (orderBy != null){
            query = orderBy(query);
        }

        return query.FirstOrDefault();
    }

    public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction() => db.Database.BeginTransaction();

    public void Commit(){
        db.SaveChanges();
    }
}
