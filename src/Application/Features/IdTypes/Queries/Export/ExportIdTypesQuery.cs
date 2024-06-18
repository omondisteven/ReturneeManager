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

namespace ReturneeManager.Application.Features.IdTypes.Queries.Export
{
    public class ExportIdTypesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportIdTypesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportIdTypesQueryHandler : IRequestHandler<ExportIdTypesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportIdTypesQueryHandler> _localizer;

        public ExportIdTypesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportIdTypesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportIdTypesQuery request, CancellationToken cancellationToken)
        {
            var idTypeFilterSpec = new IdTypeFilterSpecification(request.SearchString);
            var idTypes = await _unitOfWork.Repository<IdType>().Entities
                .Specify(idTypeFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(idTypes, mappers: new Dictionary<string, Func<IdType, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description },
            }, sheetName: _localizer["IdTypes"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
