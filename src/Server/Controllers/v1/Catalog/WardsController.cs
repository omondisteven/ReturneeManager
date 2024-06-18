using ReturneeManager.Application.Features.Wards.Queries.GetAll;
using ReturneeManager.Application.Features.Wards.Queries.GetById;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Wards.Commands.AddEdit;
using ReturneeManager.Application.Features.Wards.Commands.Delete;
using ReturneeManager.Application.Features.Wards.Queries.Export;
namespace ReturneeManager.Server.Controllers.v1.Catalog
{
    public class WardsController : BaseApiController<WardsController>
    {
        /// <summary>
        /// Get All Wards
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Wards.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var wards = await _mediator.Send(new GetAllWardsQuery());
            return Ok(wards);
        }

        /// <summary>
        /// Get a Ward By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Wards.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ward = await _mediator.Send(new GetWardByIdQuery() { Id = id });
            return Ok(ward);
        }

        /// <summary>
        /// Create/Update a Ward
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Wards.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditWardCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Ward
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Wards.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteWardCommand { Id = id }));
        }

        /// <summary>
        /// Search Wards and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Wards.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportWardsQuery(searchString)));
        }
    }
}
