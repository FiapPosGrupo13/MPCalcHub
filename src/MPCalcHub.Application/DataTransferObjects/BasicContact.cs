using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Application.DataTransferObjects;

public class BasicContact
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("ddd")]
    public int DDD { get; set; }

    [Phone(ErrorMessage = "O número de telefone inserido não é válido.")]
    [Required(ErrorMessage = "O campo de telefone é obrigatório.")]
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    [Required(ErrorMessage = "O campo de e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail inserido não é válido.")]
    public string Email { get; set; }

    public BasicContact() : base() { }

    public BasicContact(string name, string email, int ddd, string phoneNumber) : this()
    {
        Name = name;
        Email = email;
        DDD = ddd;
        PhoneNumber = phoneNumber;
    }
}