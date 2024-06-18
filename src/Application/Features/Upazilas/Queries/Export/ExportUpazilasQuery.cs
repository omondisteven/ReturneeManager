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

namespace ReturneeManager.Application.Features.Upazilas.Queries.Export
{
    public class ExportUpazilasQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportUpazilasQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportUpazilasQueryHandler : IRequestHandler<ExportUpazilasQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportUpazilasQueryHandler> _localizer;

        public ExportUpazilasQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportUpazilasQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportUpazilasQuery request, CancellationToken cancellationToken)
        {
            var upazilaFilterSpec = new UpazilaFilterSpecification(request.SearchString);
            var upazilas = await _unitOfWork.Repository<Upazila>().Entities
                .Specify(upazilaFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(upazilas, mappers: new Dictionary<string, Func<Upazila, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
            }, sheetName: _localizer["Upazilas"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
