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

    public override async Task<User> Add(User entity)
    {
        var user = await userRepository.GetByEmail(entity.Email);

        if (user != null)
            throw new Exception("O usuário já existe.");

        var xpto = await base.Add(entity);

        return xpto;
    }

    public async Task<User> GetByEmail(string email)
    {
        return await userRepository.GetByEmail(email);
    }
}