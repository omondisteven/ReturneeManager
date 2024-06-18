using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly IRepositoryAsync<Division, int> _repository;

        public DivisionRepository(IRepositoryAsync<Division, int> repository)
        {
            _repository = repository;
        }
    }
}