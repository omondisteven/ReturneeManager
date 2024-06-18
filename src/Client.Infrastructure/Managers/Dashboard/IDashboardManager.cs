using ReturneeManager.Shared.Wrapper;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Dashboards.Queries.GetData;

namespace ReturneeManager.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}