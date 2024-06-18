using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class WardRepository : IWardRepository
    {
        private readonly IRepositoryAsync<Ward, int> _repository;

        public WardRepository(IRepositoryAsync<Ward, int> repository)
        {
            _repository = repository;
        }
    }
}