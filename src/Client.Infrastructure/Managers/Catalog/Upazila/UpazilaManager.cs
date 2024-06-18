using ReturneeManager.Application.Features.Upazilas.Queries.GetAll;
using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Upazilas.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Upazila
{
    public class UpazilaManager : IUpazilaManager
    {
        private readonly HttpClient _httpClient;

        public UpazilaManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.UpazilasEndpoints.Export
                : Routes.UpazilasEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.UpazilasEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllUpazilasResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.UpazilasEndpoints.GetAll);
            return await response.ToResult<List<GetAllUpazilasResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditUpazilaCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UpazilasEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}