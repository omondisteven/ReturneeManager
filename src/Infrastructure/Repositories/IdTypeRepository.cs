using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class IdTypeRepository : IIdTypeRepository
    {
        private readonly IRepositoryAsync<IdType, int> _repository;

        public IdTypeRepository(IRepositoryAsync<IdType, int> repository)
        {
            _repository = repository;
        }
    }
}