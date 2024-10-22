using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Infrastructure.Data.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Password).IsRequired(false).HasMaxLength(256);
        builder.Property(u => u.PermissionLevel).HasDefaultValue(PermissionLevel.Guest).HasColumnType("int");
        builder.Property(u => u.DDD).IsRequired(false).HasMaxLength(3);
        builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(9);
    }
}