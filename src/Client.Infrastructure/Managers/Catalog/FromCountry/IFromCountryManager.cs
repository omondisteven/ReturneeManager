using ReturneeManager.Application.Features.FromCountries.Queries.GetAll;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.FromCountries.Commands.AddEdit;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.FromCountry
{
    public interface IFromCountryManager : IManager
    {
        Task<IResult<List<GetAllFromCountriesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditFromCountryCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}