using Microsoft.AspNetCore.Mvc.Filters;
using MPCalcHub.Domain.Entities;

namespace MPCalcHub.Api.Filters;

public class UserFilter(UserData userData) : IAuthorizationFilter
{
    private readonly UserData _userData = userData;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        

        if (user?.Claims?.Count() > 0)
        {
            _userData.Set(user);
        }
    } 
}
