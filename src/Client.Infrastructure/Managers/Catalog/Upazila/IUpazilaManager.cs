using ReturneeManager.Application.Features.Upazilas.Queries.GetAll;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Upazilas.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Upazila
{
    public interface IUpazilaManager : IManager
    {
        Task<IResult<List<GetAllUpazilasResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditUpazilaCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}