using System.Text.Json.Serialization;
using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Application.DataTransferObjects;

public class BasicContact
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("ddd")]
    public string DDD { get; set; }

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }

    public BasicContact() : base() { }

    public BasicContact(string name, string email, string ddd, string phoneNumber) : this()
    {
        Name = name;
        Email = email;
        DDD = ddd;
        PhoneNumber = phoneNumber;
    }
}