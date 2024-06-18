﻿using AutoMapper;
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

namespace ReturneeManager.Application.Features.Genders.Queries.GetAll
{
    public class GetAllGendersQuery : IRequest<Result<List<GetAllGendersResponse>>>
    {
        public GetAllGendersQuery()
        {
        }
    }

    internal class GetAllGendersCachedQueryHandler : IRequestHandler<GetAllGendersQuery, Result<List<GetAllGendersResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllGendersCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllGendersResponse>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Gender>>> getAllGenders = () => _unitOfWork.Repository<Gender>().GetAllAsync();
            var genderList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllGendersCacheKey, getAllGenders);
            var mappedGenders = _mapper.Map<List<GetAllGendersResponse>>(genderList);
            return await Result<List<GetAllGendersResponse>>.SuccessAsync(mappedGenders);
        }
    }
}