using ReturneeManager.Application.Features.FromCountries.Queries.GetAll;
using ReturneeManager.Application.Features.FromCountries.Queries.GetById;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.FromCountries.Commands.AddEdit;
using ReturneeManager.Application.Features.FromCountries.Commands.Delete;
using ReturneeManager.Application.Features.FromCountries.Queries.Export;
namespace ReturneeManager.Server.Controllers.v1.Catalog
{
    public class FromCountriesController : BaseApiController<FromCountriesController>
    {
        /// <summary>
        /// Get All FromCountries
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.FromCountries.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fromCountries = await _mediator.Send(new GetAllFromCountriesQuery());
            return Ok(fromCountries);
        }

        /// <summary>
        /// Get a FromCountry By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.FromCountries.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fromCountry = await _mediator.Send(new GetFromCountryByIdQuery() { Id = id });
            return Ok(fromCountry);
        }

        /// <summary>
        /// Create/Update a FromCountry
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.FromCountries.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditFromCountryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a FromCountry
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.FromCountries.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteFromCountryCommand { Id = id }));
        }

        /// <summary>
        /// Search FromCountries and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.FromCountries.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportFromCountriesQuery(searchString)));
        }
    }
}
