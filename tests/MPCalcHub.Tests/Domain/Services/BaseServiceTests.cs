using MPCalcHub.Domain.Entities;
using MPCalcHub.Infrastructure.Data;
using MPCalcHub.Tests.Shared.DataBase;

namespace MPCalcHub.Tests.Domain.Services;

public abstract class BaseServiceTests : IDisposable
{
    protected readonly ApplicationDBContext _context;
    protected readonly UserData _userData;

    protected BaseServiceTests()
    {
        var fixture = new TestDatabaseFixture();
        _context = fixture.DbContext;
        _userData = new UserData();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context?.Dispose();
    }
}