using ReturneeManager.Application.Features.IdTypes.Queries.GetAll;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.IdTypes.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.IdType
{
    public interface IIdTypeManager : IManager
    {
        Task<IResult<List<GetAllIdTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditIdTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}