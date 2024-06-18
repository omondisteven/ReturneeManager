using ReturneeManager.Application.Features.IdTypes.Queries.GetAll;
using ReturneeManager.Application.Features.IdTypes.Queries.GetById;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.IdTypes.Commands.AddEdit;
using ReturneeManager.Application.Features.IdTypes.Commands.Delete;
using ReturneeManager.Application.Features.IdTypes.Queries.Export;
namespace ReturneeManager.Server.Controllers.v1.Catalog
{
    public class IdTypesController : BaseApiController<IdTypesController>
    {
        /// <summary>
        /// Get All IdTypes
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.IdTypes.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var idTypes = await _mediator.Send(new GetAllIdTypesQuery());
            return Ok(idTypes);
        }

        /// <summary>
        /// Get a IdType By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.IdTypes.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var idType = await _mediator.Send(new GetIdTypeByIdQuery() { Id = id });
            return Ok(idType);
        }

        /// <summary>
        /// Create/Update a IdType
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.IdTypes.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditIdTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a IdType
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.IdTypes.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteIdTypeCommand { Id = id }));
        }

        /// <summary>
        /// Search IdTypes and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.IdTypes.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportIdTypesQuery(searchString)));
        }
    }
}
