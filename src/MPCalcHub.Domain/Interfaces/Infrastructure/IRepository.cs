using MPCalcHub.Domain.Entities.Interfaces;

namespace MPCalcHub.Domain.Interfaces.Infrastructure;

public interface IRepository<T> : IDisposable where T : class, IBaseEntity
{
    IEnumerable<T> GetAll();
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
}