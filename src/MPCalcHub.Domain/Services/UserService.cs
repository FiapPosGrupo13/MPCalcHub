using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;

namespace MPCalcHub.Domain.Services;

public class UserService(IUserRepository userRepository) : BaseService<User>(userRepository), IUserService
{
    public async Task<User> GetById(Guid id)
    {
        return await userRepository.GetById(id, false, false);
    }
}