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

namespace ReturneeManager.Application.Features.Districts.Queries.Export
{
    public class ExportDistrictsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportDistrictsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportDistrictsQueryHandler : IRequestHandler<ExportDistrictsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportDistrictsQueryHandler> _localizer;

        public ExportDistrictsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportDistrictsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportDistrictsQuery request, CancellationToken cancellationToken)
        {
            var districtFilterSpec = new DistrictFilterSpecification(request.SearchString);
            var districts = await _unitOfWork.Repository<District>().Entities
                .Specify(districtFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(districts, mappers: new Dictionary<string, Func<District, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
            }, sheetName: _localizer["Districts"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
