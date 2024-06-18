using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Constants.Application;
using ReturneeManager.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.FromCountries.Queries.GetAll
{
    public class GetAllFromCountriesQuery : IRequest<Result<List<GetAllFromCountriesResponse>>>
    {
        public GetAllFromCountriesQuery()
        {
        }
    }

    internal class GetAllFromCountriesCachedQueryHandler : IRequestHandler<GetAllFromCountriesQuery, Result<List<GetAllFromCountriesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllFromCountriesCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllFromCountriesResponse>>> Handle(GetAllFromCountriesQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<FromCountry>>> getAllFromCountries = () => _unitOfWork.Repository<FromCountry>().GetAllAsync();
            var fromCountryList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllFromCountriesCacheKey, getAllFromCountries);
            var mappedFromCountries = _mapper.Map<List<GetAllFromCountriesResponse>>(fromCountryList);
            return await Result<List<GetAllFromCountriesResponse>>.SuccessAsync(mappedFromCountries);
        }
    }
}