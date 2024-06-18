using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.Divisions.Queries.GetById
{
    public class GetDivisionByIdQuery : IRequest<Result<GetDivisionByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPersonByIdQueryHandler : IRequestHandler<GetDivisionByIdQuery, Result<GetDivisionByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetDivisionByIdResponse>> Handle(GetDivisionByIdQuery query, CancellationToken cancellationToken)
        {
            var division = await _unitOfWork.Repository<Division>().GetByIdAsync(query.Id);
            var mappedDivision = _mapper.Map<GetDivisionByIdResponse>(division);
            return await Result<GetDivisionByIdResponse>.SuccessAsync(mappedDivision);
        }
    }
}