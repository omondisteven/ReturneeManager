using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Application.Interfaces.Services;
using ReturneeManager.Domain.Entities.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReturneeManager.Application.Extensions;
using ReturneeManager.Application.Specifications.Catalog;
using ReturneeManager.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace ReturneeManager.Application.Features.Persons.Queries.Export
{
    public class ExportPersonsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportPersonsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportPersonsQueryHandler : IRequestHandler<ExportPersonsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportPersonsQueryHandler> _localizer;

        public ExportPersonsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportPersonsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportPersonsQuery request, CancellationToken cancellationToken)
        {
            var personFilterSpec = new PersonFilterSpecification(request.SearchString);
            var persons = await _unitOfWork.Repository<Person>().Entities
                .Specify(personFilterSpec)
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(persons, mappers: new Dictionary<string, Func<Person, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Id Type"], item => item.IdType },
                { _localizer["District"], item => item.District },
            }, sheetName: _localizer["Persons"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}