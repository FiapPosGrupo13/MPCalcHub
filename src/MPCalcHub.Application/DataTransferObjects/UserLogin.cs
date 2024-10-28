using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MPCalcHub.Application.DataTransferObjects
{
    public class UserLogin
    {
        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "A senha deve ter pelo menos 8 caracteres, incluindo uma letra maiúscula, um número e um caractere especial.")]
        public string Password { get; set; }
    }
}