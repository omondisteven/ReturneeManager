﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReturneeManager.Application.Extensions;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Application.Interfaces.Services;
using ReturneeManager.Application.Specifications.Catalog;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace ReturneeManager.Application.Features.Genders.Queries.Export
{
    public class ExportGendersQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportGendersQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportGendersQueryHandler : IRequestHandler<ExportGendersQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportGendersQueryHandler> _localizer;

        public ExportGendersQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportGendersQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportGendersQuery request, CancellationToken cancellationToken)
        {
            var genderFilterSpec = new GenderFilterSpecification(request.SearchString);
            var genders = await _unitOfWork.Repository<Gender>().Entities
                .Specify(genderFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(genders, mappers: new Dictionary<string, Func<Gender, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
            }, sheetName: _localizer["Genders"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
