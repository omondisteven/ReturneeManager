using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.Wards.Queries.GetById
{
    public class GetWardByIdQuery : IRequest<Result<GetWardByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPersonByIdQueryHandler : IRequestHandler<GetWardByIdQuery, Result<GetWardByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetWardByIdResponse>> Handle(GetWardByIdQuery query, CancellationToken cancellationToken)
        {
            var ward = await _unitOfWork.Repository<Ward>().GetByIdAsync(query.Id);
            var mappedWard = _mapper.Map<GetWardByIdResponse>(ward);
            return await Result<GetWardByIdResponse>.SuccessAsync(mappedWard);
        }
    }
}