using ReturneeManager.Application.Features.Documents.Commands.AddEdit;
using ReturneeManager.Application.Features.Documents.Queries.GetAll;
using ReturneeManager.Application.Requests.Documents;
using ReturneeManager.Shared.Wrapper;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Documents.Queries.GetById;

namespace ReturneeManager.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}