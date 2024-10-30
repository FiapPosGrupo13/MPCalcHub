using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Domain.Entities;

public class UserData : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; }

    public void Set(ClaimsPrincipal user)
    {
        var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Id = string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
        Email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        Name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }
}