using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Data.Repository;

public interface IRepositoryBase <TEntity> where TEntity : class
{
    bool Exist(Expression<Func<TEntity, bool>> filter);

    Task<string> GenKey();

    Task CreateAsync(TEntity entity);

    void Create(TEntity entity);

    Task CreateRangeAsync(IEnumerable<TEntity> entities);

    List<TEntity> ReadAll();

    IQueryable<TEntity> GetQuery();

    Task<List<TEntity>> ReadAllAsync();

    Task<List<TEntity>> PagingAsync(int page = 0, int count = 0, bool disableTracking = true);

    Task<TEntity> FindByIdAsync(object id);

    Task<TEntity> FindFirstByConditionAsync(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true);

    Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true, int page = 0, int count = 0);

    IQueryable<TEntity> FindQueryByCondition(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true, int page = 0, int count = 0);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);
}
