using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using MPCalcHub.Domain.Entities;

namespace MPCalcHub.Api.Filters;

public class UserFilter(UserData userData, IMemoryCache cache) : IAuthorizationFilter
{
    private readonly UserData _userData = userData;
    private readonly IMemoryCache _cache = cache;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.Filters.Any(f => f is SkipUserFilterAttribute))
            return;
        
        var user = context.HttpContext.User;
        var userId = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (_cache.TryGetValue(Guid.Parse(userId), out string? userCache) && user?.Claims?.Count() > 0)
            _userData.Set(user);
        else
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
