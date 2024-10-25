using System.Text.Json.Serialization;
using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Application.DataTransferObjects;

public class BasicUser
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("permission_level")]
    public PermissionLevel PermissionLevel { get; set; }

    [JsonPropertyName("ddd")]
    public string DDD { get; set; }

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }

    public BasicUser() : base() { }

    public BasicUser(string name, string email, string password, PermissionLevel permissionLevel, string ddd, string phoneNumber) : this()
    {
        Name = name;
        Email = email;
        Password = password;
        PermissionLevel = permissionLevel;
        DDD = ddd;
        PhoneNumber = phoneNumber;
    }
}