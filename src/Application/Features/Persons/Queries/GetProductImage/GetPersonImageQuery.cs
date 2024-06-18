using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.Persons.Queries.GetPersonImage
{
    public class GetPersonImageQuery : IRequest<Result<string>>
    {
        public int Id { get; set; }

        public GetPersonImageQuery(int personId)
        {
            Id = personId;
        }
    }

    internal class GetPersonImageQueryHandler : IRequestHandler<GetPersonImageQuery, Result<string>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetPersonImageQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(GetPersonImageQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Person>().Entities.Where(p => p.Id == request.Id).Select(a => a.ImageDataURL).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}