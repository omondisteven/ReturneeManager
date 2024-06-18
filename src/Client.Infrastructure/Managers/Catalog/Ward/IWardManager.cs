using ReturneeManager.Application.Features.Wards.Queries.GetAll;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Wards.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Ward
{
    public interface IWardManager : IManager
    {
        Task<IResult<List<GetAllWardsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditWardCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}