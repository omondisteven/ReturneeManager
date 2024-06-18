using ReturneeManager.Application.Features.Persons.Commands.AddEdit;
using ReturneeManager.Application.Features.Persons.Queries.GetAllPaged;
using ReturneeManager.Application.Requests.Catalog;
using ReturneeManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Person
{
    public interface IPersonManager : IManager
    {
        Task<PaginatedResult<GetAllPagedPersonsResponse>> GetPersonsAsync(GetAllPagedPersonsRequest request);

        Task<IResult<string>> GetPersonImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditPersonCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}