using ReturneeManager.Client.Infrastructure.Extensions;
using ReturneeManager.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Dashboards.Queries.GetData;

namespace ReturneeManager.Client.Infrastructure.Managers.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        public DashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataResponse>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DashboardEndpoints.GetData);
            var data = await response.ToResult<DashboardDataResponse>();
            return data;
        }
    }
}