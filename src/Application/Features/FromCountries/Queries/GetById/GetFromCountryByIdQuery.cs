using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.FromCountries.Queries.GetById
{
    public class GetFromCountryByIdQuery : IRequest<Result<GetFromCountryByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPersonByIdQueryHandler : IRequestHandler<GetFromCountryByIdQuery, Result<GetFromCountryByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetFromCountryByIdResponse>> Handle(GetFromCountryByIdQuery query, CancellationToken cancellationToken)
        {
            var fromCountry = await _unitOfWork.Repository<FromCountry>().GetByIdAsync(query.Id);
            var mappedFromCountry = _mapper.Map<GetFromCountryByIdResponse>(fromCountry);
            return await Result<GetFromCountryByIdResponse>.SuccessAsync(mappedFromCountry);
        }
    }
}