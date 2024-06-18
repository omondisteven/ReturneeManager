using System.Threading.Tasks;

namespace ReturneeManager.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsBrandUsed(int brandId);

        //adding IdType
        Task<bool> IsIdTypeUsed(int idTypeId);

        //adding Gender
        Task<bool> IsGenderUsed(int GenderId);

        //adding District
        Task<bool> IsDistrictUsed(int DistrictId);

         //adding Division
        Task<bool> IsDivisionUsed(int DivisionId);

        //adding Upazila
        Task<bool> IsUpazilaUsed(int UpazilaId);

        //adding Ward
        Task<bool> IsWardUsed(int WardId);

        //adding FromCountry
        Task<bool> IsFromCountryUsed(int FromCountryId);
    }
}