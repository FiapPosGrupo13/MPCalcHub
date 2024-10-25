using Microsoft.AspNetCore.Mvc;

namespace MPCalcHub.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }
}
