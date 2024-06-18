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

namespace ReturneeManager.Application.Features.Upazilas.Queries.GetAll
{
    public class GetAllUpazilasQuery : IRequest<Result<List<GetAllUpazilasResponse>>>
    {
        public GetAllUpazilasQuery()
        {
        }
    }

    internal class GetAllUpazilasCachedQueryHandler : IRequestHandler<GetAllUpazilasQuery, Result<List<GetAllUpazilasResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllUpazilasCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllUpazilasResponse>>> Handle(GetAllUpazilasQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Upazila>>> getAllUpazilas = () => _unitOfWork.Repository<Upazila>().GetAllAsync();
            var upazilaList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllUpazilasCacheKey, getAllUpazilas);
            var mappedUpazilas = _mapper.Map<List<GetAllUpazilasResponse>>(upazilaList);
            return await Result<List<GetAllUpazilasResponse>>.SuccessAsync(mappedUpazilas);
        }
    }
}