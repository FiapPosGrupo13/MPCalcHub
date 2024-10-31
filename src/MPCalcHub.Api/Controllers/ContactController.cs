using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;
using static MPCalcHub.Api.Constants.AppConstants;

namespace MPCalcHub.Api.Controllers
{
    /// <summary>
    /// Contact controller
    /// </summary>
    [Route("contacts")]
    public class ContactController(ILogger<ContactController> logger, IContactApplicationService ContactApplicationService) : BaseController(logger)
    {
        /// <summary>
        /// Criar um novo contato
        /// </summary>
        /// <remarks>
        /// Obs: Não é necessário informar o Id, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, Removed, RemovedAt, RemovedBy
        /// </remarks>
        /// <param name="contact">Objeto com as propriedades para criar um novo contato</param>
        /// <returns>Um objeto do contato criado</returns>
        [HttpPost]
        //[Authorize(Policy = Policies.SuperOrModerator)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        public async Task<object> Create([FromBody] BasicContact contact)
        {
            try
            {
                var entity = await ContactApplicationService.Add(contact);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Buscar contatos por DDD
        /// </summary>
        /// <remarks>
        /// Obs: Não é necessário informar o Id, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, Removed, RemovedAt, RemovedBy
        /// </remarks>
        /// <param name="ddd">DDD para busca de contatos</param>
        /// <returns>Um objeto do contato criado</returns>
        [HttpGet("find")]
        // [Authorize(Policy = Policies.SuperOrModerator)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        public async Task<object> Get([FromQuery] string ddd)
        {
            try
            {
                var entity = await ContactApplicationService.FindBy(c => c.DDD == ddd);
                return Ok(entity); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Excluir contato
        /// </summary>
        /// <param name="contactId">Id do contato</param>
        /// <returns>Contato excluído com sucesso</returns>
        [HttpDelete]
        //[Authorize(Policy = Policies.SuperOrModerator)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        public object Delete(Guid contactId)
        {
            try
            {
                ContactApplicationService.Remove(contactId);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Editar um contato
        /// </summary>
        /// <remarks>
        /// Obs: Não é necessário informar o Id, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, Removed, RemovedAt, RemovedBy
        /// </remarks>
        /// <param name="contact">Objeto com as propriedades para editar um contato</param>
        /// <returns>Um objeto do contato criado</returns>
        [HttpPut]
        //[Authorize(Policy = Policies.SuperOrModerator)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        public async Task<object> Update([FromBody] Contact contact)
        {
            try
            {
                var entity = await ContactApplicationService.Update(contact);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
