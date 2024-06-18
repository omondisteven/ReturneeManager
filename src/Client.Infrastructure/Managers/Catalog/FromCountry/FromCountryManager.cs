using ReturneeManager.Application.Features.FromCountries.Queries.GetAll;
using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.FromCountries.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.FromCountry
{
    public class FromCountryManager : IFromCountryManager
    {
        private readonly HttpClient _httpClient;

        public FromCountryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.FromCountriesEndpoints.Export
                : Routes.FromCountriesEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.FromCountriesEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllFromCountriesResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.FromCountriesEndpoints.GetAll);
            return await response.ToResult<List<GetAllFromCountriesResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditFromCountryCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.FromCountriesEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}