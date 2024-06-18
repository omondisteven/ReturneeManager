using ReturneeManager.Application.Features.Divisions.Queries.GetAll;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Divisions.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Division
{
    public interface IDivisionManager : IManager
    {
        Task<IResult<List<GetAllDivisionsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDivisionCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}