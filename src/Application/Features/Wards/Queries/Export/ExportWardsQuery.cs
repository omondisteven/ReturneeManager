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

namespace ReturneeManager.Application.Features.Wards.Queries.Export
{
    public class ExportWardsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportWardsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportWardsQueryHandler : IRequestHandler<ExportWardsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportWardsQueryHandler> _localizer;

        public ExportWardsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportWardsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportWardsQuery request, CancellationToken cancellationToken)
        {
            var wardFilterSpec = new WardFilterSpecification(request.SearchString);
            var wards = await _unitOfWork.Repository<Ward>().Entities
                .Specify(wardFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(wards, mappers: new Dictionary<string, Func<Ward, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
            }, sheetName: _localizer["Wards"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
