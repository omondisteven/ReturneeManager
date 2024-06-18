using ReturneeManager.Application.Features.Wards.Queries.GetAll;
using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Wards.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Ward
{
    public class WardManager : IWardManager
    {
        private readonly HttpClient _httpClient;

        public WardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.WardsEndpoints.Export
                : Routes.WardsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.WardsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllWardsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.WardsEndpoints.GetAll);
            return await response.ToResult<List<GetAllWardsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditWardCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.WardsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}