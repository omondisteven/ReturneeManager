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

namespace ReturneeManager.Application.Features.Divisions.Queries.GetAll
{
    public class GetAllDivisionsQuery : IRequest<Result<List<GetAllDivisionsResponse>>>
    {
        public GetAllDivisionsQuery()
        {
        }
    }

    internal class GetAllDivisionsCachedQueryHandler : IRequestHandler<GetAllDivisionsQuery, Result<List<GetAllDivisionsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllDivisionsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllDivisionsResponse>>> Handle(GetAllDivisionsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Division>>> getAllDivisions = () => _unitOfWork.Repository<Division>().GetAllAsync();
            var divisionList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllDivisionsCacheKey, getAllDivisions);
            var mappedDivisions = _mapper.Map<List<GetAllDivisionsResponse>>(divisionList);
            return await Result<List<GetAllDivisionsResponse>>.SuccessAsync(mappedDivisions);
        }
    }
}