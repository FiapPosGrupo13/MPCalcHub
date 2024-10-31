using MPCalcHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MPCalcHub.Application.DataTransferObjects
{
    public class Contact : BaseModel
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ddd")]
        public int DDD { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        public Contact() : base() { }

        public Contact(Guid? id, DateTime createdAt, Guid createdBy, DateTime? updatedAt, Guid? updatedBy, bool removed, DateTime? removedAt, Guid? removedBy, string name, string phoneNumber, string email)
        {
            Id = id;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            Removed = removed;
            RemovedAt = removedAt;
            RemovedBy = removedBy;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }

}
