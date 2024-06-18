using ReturneeManager.Application.Features.Genders.Queries.GetAll;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Genders.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Gender
{
    public interface IGenderManager : IManager
    {
        Task<IResult<List<GetAllGendersResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditGenderCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}