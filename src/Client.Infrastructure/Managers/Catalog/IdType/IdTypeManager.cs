using ReturneeManager.Application.Features.IdTypes.Queries.GetAll;
using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.IdTypes.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.IdType
{
    public class IdTypeManager : IIdTypeManager
    {
        private readonly HttpClient _httpClient;

        public IdTypeManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.IdTypesEndpoints.Export
                : Routes.IdTypesEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.IdTypesEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllIdTypesResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.IdTypesEndpoints.GetAll);
            return await response.ToResult<List<GetAllIdTypesResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditIdTypeCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.IdTypesEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}