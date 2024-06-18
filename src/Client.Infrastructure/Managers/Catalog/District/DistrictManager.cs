using ReturneeManager.Application.Features.Districts.Queries.GetAll;
using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Districts.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.District
{
    public class DistrictManager : IDistrictManager
    {
        private readonly HttpClient _httpClient;

        public DistrictManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.DistrictsEndpoints.Export
                : Routes.DistrictsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.DistrictsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllDistrictsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DistrictsEndpoints.GetAll);
            return await response.ToResult<List<GetAllDistrictsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditDistrictCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DistrictsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}