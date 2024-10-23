using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Entities.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;

namespace MPCalcHub.Infrastructure.Data.Repositories;

public abstract class BaseRepository<T> : BaseExpressionService<T>, IRepository<T> where T : class, IBaseEntity
{
    public BaseRepository(ApplicationDBContext context) : base(context) { }

    public abstract Task<T> GetById(Guid id, bool include, bool tracking);

    public virtual async Task<T> Add(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<T> Update(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task Delete(T entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
        await Context.SaveChangesAsync();

        return entities;
    }

    public virtual void Dispose()
    {
        Context.Dispose();
    }
}