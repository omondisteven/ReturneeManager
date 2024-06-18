using ReturneeManager.Application.Features.Upazilas.Queries.GetAll;
using ReturneeManager.Application.Features.Upazilas.Queries.GetById;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Upazilas.Commands.AddEdit;
using ReturneeManager.Application.Features.Upazilas.Commands.Delete;
using ReturneeManager.Application.Features.Upazilas.Queries.Export;
namespace ReturneeManager.Server.Controllers.v1.Catalog
{
    public class UpazilasController : BaseApiController<UpazilasController>
    {
        /// <summary>
        /// Get All Upazilas
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Upazilas.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var upazilas = await _mediator.Send(new GetAllUpazilasQuery());
            return Ok(upazilas);
        }

        /// <summary>
        /// Get a Upazila By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Upazilas.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var upazila = await _mediator.Send(new GetUpazilaByIdQuery() { Id = id });
            return Ok(upazila);
        }

        /// <summary>
        /// Create/Update a Upazila
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Upazilas.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditUpazilaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Upazila
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Upazilas.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteUpazilaCommand { Id = id }));
        }

        /// <summary>
        /// Search Upazilas and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Upazilas.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportUpazilasQuery(searchString)));
        }
    }
}
