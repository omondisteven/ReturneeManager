using ReturneeManager.Application.Features.Divisions.Queries.GetAll;
using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Divisions.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Division
{
    public class DivisionManager : IDivisionManager
    {
        private readonly HttpClient _httpClient;

        public DivisionManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.DivisionsEndpoints.Export
                : Routes.DivisionsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.DivisionsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllDivisionsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DivisionsEndpoints.GetAll);
            return await response.ToResult<List<GetAllDivisionsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditDivisionCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DivisionsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}