using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Data.Repository;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity :class
{
    private readonly DbSet<TEntity> dbSet;

    public RepositoryBase(ConsoleContext context) =>  dbSet = context.Set<TEntity>();
    
    
    public bool Exist(Expression<Func<TEntity, bool>> filter) => dbSet.Where(filter).Any();

    //? If your key be string you can use it to check key is not exist
    public async Task<string> GenKey()
    {
        string Id = Guid.NewGuid().ToString();
        while(await dbSet.FindAsync(Id) != null)
        {
            Id = Guid.NewGuid().ToString();
        }
        return Id;
    }

    public async Task CreateAsync(TEntity entity) =>  await dbSet.AddAsync(entity);

    public void Create(TEntity entity) =>  dbSet.Add(entity);

    public async Task CreateRangeAsync(IEnumerable<TEntity> entities) =>  await dbSet.AddRangeAsync(entities);

    public List<TEntity> ReadAll() => dbSet.ToList();

    public IQueryable<TEntity> GetQuery() => dbSet;

    public async Task<List<TEntity>> ReadAllAsync() => await dbSet.ToListAsync();

    public async Task<List<TEntity>> PagingAsync(int page = 0, int count = 0, bool disableTracking = true) =>  (count != 0 && page != 0) ? disableTracking ? await dbSet.AsNoTracking().Skip((page - 1)* count).Take(count).ToListAsync() :  await dbSet.Skip((page - 1)* count).Take(count).ToListAsync() : disableTracking ? await dbSet.AsNoTracking().ToListAsync() : await dbSet.ToListAsync();

    public async Task<TEntity> FindByIdAsync(object id) =>  await dbSet.FindAsync(id);

    public async Task<TEntity> FindFirstByConditionAsync(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true)
    {
        IQueryable<TEntity> query = dbSet;

        query = disableTracking ? query.AsNoTracking() : query.AsTracking();

        if (include != null) query = include(query);

        if (filter != null) query = query.Where(filter);

        if (orderBy != null) _ = orderBy(query);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true, int page = 0, int count = 0)
    {
        IQueryable<TEntity> query = dbSet;
        
        query = disableTracking ? query.AsNoTracking() : query.AsTracking();

        if (include != null) query = include(query);

        if (filter != null) query = query.Where(filter);

        if (orderBy != null) _ = orderBy(query);

        return query.Any() ? (count != 0 && page != 0) ? await query.Skip((page - 1)* count).Take(count).ToListAsync() : await query.ToListAsync() : new();
    }

    public IQueryable<TEntity> FindQueryByCondition(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true, int page = 0, int count = 0)
    {
        IQueryable<TEntity> query = dbSet;
        
        query = disableTracking ? query.AsNoTracking() : query.AsTracking();

        if (include != null) query = include(query);

        if (filter != null) query = query.Where(filter);

        if (orderBy != null) _ = orderBy(query);

        return query.Any() ? (count != 0 && page != 0) ? query.Skip((page - 1)* count).Take(count) : query : null;
    }

    public void Update(TEntity entity) =>  dbSet.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) =>  dbSet.UpdateRange(entities);

    public void Delete(TEntity entity) =>  dbSet.Remove(entity);

    public void DeleteRange(IEnumerable<TEntity> entities) =>  dbSet.RemoveRange(entities);
}
