using ReturneeManager.Application.Features.Persons.Commands.AddEdit;
using ReturneeManager.Application.Features.Persons.Queries.GetAllPaged;
using ReturneeManager.Application.Requests.Catalog;
using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Person
{
    public class PersonManager : IPersonManager
    {
        private readonly HttpClient _httpClient;

        public PersonManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.PersonsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.PersonsEndpoints.Export
                : Routes.PersonsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<string>> GetPersonImageAsync(int id)
        {
            var response = await _httpClient.GetAsync(Routes.PersonsEndpoints.GetPersonImage(id));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedPersonsResponse>> GetPersonsAsync(GetAllPagedPersonsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.PersonsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedPersonsResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditPersonCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.PersonsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
        
    }
}