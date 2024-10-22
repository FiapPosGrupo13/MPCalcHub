using MPCalcHub.Domain.Entities;

namespace MPCalcHub.Domain.Interfaces.Infrastructure;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetById(Guid id, bool include, bool tracking);
}