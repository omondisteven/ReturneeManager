using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.Districts.Queries.GetById
{
    public class GetDistrictByIdQuery : IRequest<Result<GetDistrictByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPersonByIdQueryHandler : IRequestHandler<GetDistrictByIdQuery, Result<GetDistrictByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetDistrictByIdResponse>> Handle(GetDistrictByIdQuery query, CancellationToken cancellationToken)
        {
            var district = await _unitOfWork.Repository<District>().GetByIdAsync(query.Id);
            var mappedDistrict = _mapper.Map<GetDistrictByIdResponse>(district);
            return await Result<GetDistrictByIdResponse>.SuccessAsync(mappedDistrict);
        }
    }
}