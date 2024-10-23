using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MPCalcHub.Domain.Enums;

namespace MPCalcHub.Application.Authorization;

public class RolesAuthorizationHandler : AuthorizationHandler<RolesRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
    {
        var roleClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;

        if (roleClaim != null && Enum.TryParse<PermissionLevel>(roleClaim, out var userRoles))
        {
            if ((userRoles & requirement.Permission) == requirement.Permission)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}
