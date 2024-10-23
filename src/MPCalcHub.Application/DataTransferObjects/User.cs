using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Application.DataTransferObjects
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PermissionLevel PermissionLevel { get; set; }
        public string? DDD { get; set; }
        public string? PhoneNumber { get; set; }

        public User() : base() { }

        public User(Guid? id, DateTime createdAt, Guid createdBy, DateTime? updatedAt, Guid? updatedBy, bool removed, DateTime? removedAt, Guid? removedBy, string name, string email, string password, PermissionLevel permissionLevel, string? ddd, string? phoneNumber) : this()
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
            Email = email;
            Password = password;
            PermissionLevel = permissionLevel;
            DDD = ddd;
            PhoneNumber = phoneNumber;
        }
    }
}