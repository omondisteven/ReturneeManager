using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class UpazilaRepository : IUpazilaRepository
    {
        private readonly IRepositoryAsync<Upazila, int> _repository;

        public UpazilaRepository(IRepositoryAsync<Upazila, int> repository)
        {
            _repository = repository;
        }
    }
}