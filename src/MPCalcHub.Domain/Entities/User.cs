using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Domain.Entities;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public PermissionLevel PermissionLevel { get; set; }
    public string? DDD { get; set; }
    public string? PhoneNumber { get; set; }

    public User() { }

    public User(string name, string email, string password, PermissionLevel permissionLevel, string? ddd, string? phoneNumber, Guid userId)
    {
        Name = name;
        Email = email;
        Password = password;
        PermissionLevel = permissionLevel;
        DDD = ddd;
        PhoneNumber = phoneNumber;

        PrepareToInsert(userId);
    }
}