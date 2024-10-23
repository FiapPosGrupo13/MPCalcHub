using MPCalcHub.Domain.Entities.Interfaces;
using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;

namespace MPCalcHub.Domain.Services
{
    public abstract class BaseService<T>(IRepository<T> repository) : IBaseService<T> where T : class, IBaseEntity
    {
        public virtual async Task<T> Add(T entity)
        {
            return await repository.Add(entity);
        }

        public async Task Delete(T entity)
        {
            await repository.Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }

        public async Task<T> Update(T entity)
        {
           return await repository.Update(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            repository.Dispose();
        }
    }
}