using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Entities.Interfaces;
using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;

namespace MPCalcHub.Domain.Services
{
    public abstract class BaseService<T>(
        IRepository<T> repository,
        UserData userData) : IBaseService<T> where T : class, IBaseEntity
    {
        protected readonly IRepository<T> _repository = repository;
        protected readonly UserData _userData = userData;

        public virtual async Task<T> Add(T entity)
        {
            return await _repository.Add(entity);
        }

        public async Task Delete(T entity)
        {
            await _repository.Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<T> Update(T entity)
        {
           return await _repository.Update(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _repository.Dispose();
        }
    }
}