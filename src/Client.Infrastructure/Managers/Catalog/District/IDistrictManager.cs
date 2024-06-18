using ReturneeManager.Application.Features.Districts.Queries.GetAll;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Districts.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.District
{
    public interface IDistrictManager : IManager
    {
        Task<IResult<List<GetAllDistrictsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDistrictCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}