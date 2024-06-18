using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly IRepositoryAsync<District, int> _repository;

        public DistrictRepository(IRepositoryAsync<District, int> repository)
        {
            _repository = repository;
        }
    }
}