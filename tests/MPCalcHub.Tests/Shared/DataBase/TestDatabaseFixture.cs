using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MPCalcHub.Infrastructure.Data;

namespace MPCalcHub.Tests.Shared.DataBase;

public class TestDatabaseFixture : IDisposable
{
    public ApplicationDBContext DbContext { get; private set; }
    private readonly SqlDockerSetup _sqlDockerSetup;

    public TestDatabaseFixture()
    {
        _sqlDockerSetup = new SqlDockerSetup();  // Inicia o Docker
        var serviceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(_sqlDockerSetup.ConnectionString))
            .BuildServiceProvider();

        DbContext = serviceProvider.GetService<ApplicationDBContext>()!;

        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        DbContext.Dispose();
        _sqlDockerSetup.Dispose();
    }
}
