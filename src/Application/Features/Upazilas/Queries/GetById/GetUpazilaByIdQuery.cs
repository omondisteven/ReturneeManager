using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.Upazilas.Queries.GetById
{
    public class GetUpazilaByIdQuery : IRequest<Result<GetUpazilaByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPersonByIdQueryHandler : IRequestHandler<GetUpazilaByIdQuery, Result<GetUpazilaByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetUpazilaByIdResponse>> Handle(GetUpazilaByIdQuery query, CancellationToken cancellationToken)
        {
            var upazila = await _unitOfWork.Repository<Upazila>().GetByIdAsync(query.Id);
            var mappedUpazila = _mapper.Map<GetUpazilaByIdResponse>(upazila);
            return await Result<GetUpazilaByIdResponse>.SuccessAsync(mappedUpazila);
        }
    }
}