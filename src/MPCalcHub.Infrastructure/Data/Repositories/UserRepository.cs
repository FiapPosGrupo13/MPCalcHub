using Microsoft.EntityFrameworkCore;
using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Interfaces.Infrastructure;

namespace MPCalcHub.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationDBContext context) : BaseRepository<User>(context), IUserRepository
{
    public override async Task<User> GetById(Guid id, bool include, bool tracking)
    {
        var query = BaseQuery(tracking);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
}