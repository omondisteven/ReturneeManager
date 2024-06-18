using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly IRepositoryAsync<Gender, int> _repository;

        public GenderRepository(IRepositoryAsync<Gender, int> repository)
        {
            _repository = repository;
        }
    }
}