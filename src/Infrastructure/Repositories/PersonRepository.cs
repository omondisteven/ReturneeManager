using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IRepositoryAsync<Person, int> _repository;

        public PersonRepository(IRepositoryAsync<Person, int> repository)
        {
            _repository = repository;
        }
       

        //IdType
        public async Task<bool> IsIdTypeUsed(int idTypeId)
        {
            return await _repository.Entities.AnyAsync(b => b.IdTypeId == idTypeId);
        }

        //Gender
        public async Task<bool> IsGenderUsed(int genderId)
        {
            return await _repository.Entities.AnyAsync(b => b.GenderId == genderId);
        }

        //District
        public async Task<bool> IsDistrictUsed(int districtId)
        {
            return await _repository.Entities.AnyAsync(b => b.DistrictId == districtId);
        }

        //Division
        public async Task<bool> IsDivisionUsed(int divisionId)
        {
            return await _repository.Entities.AnyAsync(b => b.DivisionId == divisionId);
        }

        //Upazila
        public async Task<bool> IsUpazilaUsed(int upazilaId)
        {
            return await _repository.Entities.AnyAsync(b => b.UpazilaId == upazilaId);
        }

        //Ward
        public async Task<bool> IsWardUsed(int wardId)
        {
            return await _repository.Entities.AnyAsync(b => b.WardId == wardId);
        }

        //FromCountry
        public async Task<bool> IsFromCountryUsed(int fromCountryId)
        {
            return await _repository.Entities.AnyAsync(b => b.FromCountryId == fromCountryId);
        }
    }
}