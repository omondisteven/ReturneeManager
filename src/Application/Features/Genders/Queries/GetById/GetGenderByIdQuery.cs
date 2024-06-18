﻿using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.Genders.Queries.GetById
{
    public class GetGenderByIdQuery : IRequest<Result<GetGenderByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetPersonByIdQueryHandler : IRequestHandler<GetGenderByIdQuery, Result<GetGenderByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetGenderByIdResponse>> Handle(GetGenderByIdQuery query, CancellationToken cancellationToken)
        {
            var gender = await _unitOfWork.Repository<Gender>().GetByIdAsync(query.Id);
            var mappedGender = _mapper.Map<GetGenderByIdResponse>(gender);
            return await Result<GetGenderByIdResponse>.SuccessAsync(mappedGender);
        }
    }
}