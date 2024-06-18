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

namespace ReturneeManager.Application.Features.Wards.Queries.GetAll
{
    public class GetAllWardsQuery : IRequest<Result<List<GetAllWardsResponse>>>
    {
        public GetAllWardsQuery()
        {
        }
    }

    internal class GetAllWardsCachedQueryHandler : IRequestHandler<GetAllWardsQuery, Result<List<GetAllWardsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllWardsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllWardsResponse>>> Handle(GetAllWardsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Ward>>> getAllWards = () => _unitOfWork.Repository<Ward>().GetAllAsync();
            var wardList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllWardsCacheKey, getAllWards);
            var mappedWards = _mapper.Map<List<GetAllWardsResponse>>(wardList);
            return await Result<List<GetAllWardsResponse>>.SuccessAsync(mappedWards);
        }
    }
}