using ReturneeManager.Application.Features.Divisions.Queries.GetAll;
using ReturneeManager.Application.Features.Divisions.Queries.GetById;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Divisions.Commands.AddEdit;
using ReturneeManager.Application.Features.Divisions.Commands.Delete;
using ReturneeManager.Application.Features.Divisions.Queries.Export;
namespace ReturneeManager.Server.Controllers.v1.Catalog
{
    public class DivisionsController : BaseApiController<DivisionsController>
    {
        /// <summary>
        /// Get All Divisions
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Divisions.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var divisions = await _mediator.Send(new GetAllDivisionsQuery());
            return Ok(divisions);
        }

        /// <summary>
        /// Get a Division By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Divisions.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var division = await _mediator.Send(new GetDivisionByIdQuery() { Id = id });
            return Ok(division);
        }

        /// <summary>
        /// Create/Update a Division
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Divisions.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditDivisionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Division
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Divisions.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDivisionCommand { Id = id }));
        }

        /// <summary>
        /// Search Divisions and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Divisions.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportDivisionsQuery(searchString)));
        }
    }
}
