using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Misc;

namespace ReturneeManager.Infrastructure.Repositories
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly IRepositoryAsync<DocumentType, int> _repository;

        public DocumentTypeRepository(IRepositoryAsync<DocumentType, int> repository)
        {
            _repository = repository;
        }
    }
}