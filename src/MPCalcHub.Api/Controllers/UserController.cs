using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Application.DataTransferObjects;

namespace MPCalcHub.Api.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("users")]
    [ApiController]
    public class UserController(ILogger<UserController> logger, IUserApplicationService userApplicationService) : BaseController(logger)
    {
        /// <summary>
        /// Criar um novo usuário
        /// </summary>
        /// <param name="user">Objeto com as propriedades para criar um novo usuário<see cref="User"/></param>
        /// <returns>Um objeto do usuaário criado <see cref="User"/></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<object> Create([FromBody] User user)
        {
            try
            {
                var entity = await userApplicationService.Add(user);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}