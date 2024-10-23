using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;

namespace MPCalcHub.Api.Controllers
{
    [Route("accounts")]
    public class AccountController(ILogger<AccountController> logger, ITokenApplicationService _tokenApplicationService) : BaseController(logger)
    {
        [HttpPost("token")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<object> GetToken([FromBody] UserLogin userLogin)
        {
            try
            {
                var token = await _tokenApplicationService.GetToken(userLogin);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized();

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}