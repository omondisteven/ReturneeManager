using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class FromCountryRepository : IFromCountryRepository
    {
        private readonly IRepositoryAsync<FromCountry, int> _repository;

        public FromCountryRepository(IRepositoryAsync<FromCountry, int> repository)
        {
            _repository = repository;
        }
    }
}