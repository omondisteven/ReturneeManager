using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.IdTypes.Queries.GetById
{
    public class GetIdTypeByIdQuery : IRequest<Result<GetIdTypeByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPersonByIdQueryHandler : IRequestHandler<GetIdTypeByIdQuery, Result<GetIdTypeByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetIdTypeByIdResponse>> Handle(GetIdTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var idType = await _unitOfWork.Repository<IdType>().GetByIdAsync(query.Id);
            var mappedIdType = _mapper.Map<GetIdTypeByIdResponse>(idType);
            return await Result<GetIdTypeByIdResponse>.SuccessAsync(mappedIdType);
        }
    }
}