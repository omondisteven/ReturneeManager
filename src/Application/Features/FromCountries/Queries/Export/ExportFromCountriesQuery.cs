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

namespace ReturneeManager.Application.Features.FromCountries.Queries.Export
{
    public class ExportFromCountriesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportFromCountriesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportFromCountriesQueryHandler : IRequestHandler<ExportFromCountriesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportFromCountriesQueryHandler> _localizer;

        public ExportFromCountriesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportFromCountriesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportFromCountriesQuery request, CancellationToken cancellationToken)
        {
            var fromCountryFilterSpec = new FromCountryFilterSpecification(request.SearchString);
            var fromCountries = await _unitOfWork.Repository<FromCountry>().Entities
                .Specify(fromCountryFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(fromCountries, mappers: new Dictionary<string, Func<FromCountry, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
            }, sheetName: _localizer["FromCountries"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
