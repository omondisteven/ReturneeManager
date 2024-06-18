using ReturneeManager.Application.Features.Districts.Queries.GetAll;
using ReturneeManager.Application.Features.Districts.Queries.GetById;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Districts.Commands.AddEdit;
using ReturneeManager.Application.Features.Districts.Commands.Delete;
using ReturneeManager.Application.Features.Districts.Queries.Export;
namespace ReturneeManager.Server.Controllers.v1.Catalog
{
    public class DistrictsController : BaseApiController<DistrictsController>
    {
        /// <summary>
        /// Get All Districts
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Districts.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var districts = await _mediator.Send(new GetAllDistrictsQuery());
            return Ok(districts);
        }

        /// <summary>
        /// Get a District By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Districts.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var district = await _mediator.Send(new GetDistrictByIdQuery() { Id = id });
            return Ok(district);
        }

        /// <summary>
        /// Create/Update a District
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Districts.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditDistrictCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a District
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Districts.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDistrictCommand { Id = id }));
        }

        /// <summary>
        /// Search Districts and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Districts.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportDistrictsQuery(searchString)));
        }
    }
}
