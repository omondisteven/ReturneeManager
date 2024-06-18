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

namespace ReturneeManager.Application.Features.IdTypes.Queries.GetAll
{
    public class GetAllIdTypesQuery : IRequest<Result<List<GetAllIdTypesResponse>>>
    {
        public GetAllIdTypesQuery()
        {
        }
    }

    internal class GetAllIdTypesCachedQueryHandler : IRequestHandler<GetAllIdTypesQuery, Result<List<GetAllIdTypesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllIdTypesCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllIdTypesResponse>>> Handle(GetAllIdTypesQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<IdType>>> getAllIdTypes = () => _unitOfWork.Repository<IdType>().GetAllAsync();
            var idTypeList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllIdTypesCacheKey, getAllIdTypes);
            var mappedIdTypes = _mapper.Map<List<GetAllIdTypesResponse>>(idTypeList);
            return await Result<List<GetAllIdTypesResponse>>.SuccessAsync(mappedIdTypes);
        }
    }
}