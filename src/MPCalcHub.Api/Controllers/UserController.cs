using Microsoft.AspNetCore.Mvc;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Application.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using MPCalcHub.Domain.Enums;
using static MPCalcHub.Api.Constants.AppConstants;

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
        /// <remarks>
        /// Obs: Não é necessário informar o Id, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, Removed, RemovedAt, RemovedBy
        /// </remarks>
        /// <param name="user">Objeto com as propriedades para criar um novo usuário</param>
        /// <returns>Um objeto do usuário criado</returns>
        [HttpPost]
        [Authorize(Policy = Policies.SuperUser)]
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