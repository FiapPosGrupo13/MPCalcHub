using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MPCalcHub.Domain.Entities;

namespace MPCalcHub.Infrastructure.Data;

public class ApplicationDBContext : DbContext
{ 
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
    }
}