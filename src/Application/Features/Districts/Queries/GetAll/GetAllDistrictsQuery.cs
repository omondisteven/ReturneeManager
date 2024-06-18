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

namespace ReturneeManager.Application.Features.Districts.Queries.GetAll
{
    public class GetAllDistrictsQuery : IRequest<Result<List<GetAllDistrictsResponse>>>
    {
        public GetAllDistrictsQuery()
        {
        }
    }

    internal class GetAllDistrictsCachedQueryHandler : IRequestHandler<GetAllDistrictsQuery, Result<List<GetAllDistrictsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllDistrictsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllDistrictsResponse>>> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<District>>> getAllDistricts = () => _unitOfWork.Repository<District>().GetAllAsync();
            var districtList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllDistrictsCacheKey, getAllDistricts);
            var mappedDistricts = _mapper.Map<List<GetAllDistrictsResponse>>(districtList);
            return await Result<List<GetAllDistrictsResponse>>.SuccessAsync(mappedDistricts);
        }
    }
}