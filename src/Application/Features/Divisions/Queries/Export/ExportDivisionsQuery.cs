using System;
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

namespace ReturneeManager.Application.Features.Divisions.Queries.Export
{
    public class ExportDivisionsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportDivisionsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportDivisionsQueryHandler : IRequestHandler<ExportDivisionsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportDivisionsQueryHandler> _localizer;

        public ExportDivisionsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportDivisionsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportDivisionsQuery request, CancellationToken cancellationToken)
        {
            var divisionFilterSpec = new DivisionFilterSpecification(request.SearchString);
            var divisions = await _unitOfWork.Repository<Division>().Entities
                .Specify(divisionFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(divisions, mappers: new Dictionary<string, Func<Division, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
            }, sheetName: _localizer["Divisions"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
