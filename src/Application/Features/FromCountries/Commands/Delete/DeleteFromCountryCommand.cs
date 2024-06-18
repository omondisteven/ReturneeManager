using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ReturneeManager.Shared.Constants.Application;

namespace ReturneeManager.Application.Features.FromCountries.Commands.Delete
{
    public class DeleteFromCountryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteFromCountryCommandHandler : IRequestHandler<DeleteFromCountryCommand, Result<int>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer<DeleteFromCountryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteFromCountryCommandHandler(IUnitOfWork<int> unitOfWork, IPersonRepository personRepository, IStringLocalizer<DeleteFromCountryCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteFromCountryCommand command, CancellationToken cancellationToken)
        {
            var isFromCountryUsed = await _personRepository.IsFromCountryUsed(command.Id);
            if (!isFromCountryUsed)
            {
                var fromCountry = await _unitOfWork.Repository<FromCountry>().GetByIdAsync(command.Id);
                if (fromCountry != null)
                {
                    await _unitOfWork.Repository<FromCountry>().DeleteAsync(fromCountry);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllFromCountriesCacheKey);
                    return await Result<int>.SuccessAsync(fromCountry.Id, _localizer["FromCountry Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["FromCountry Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}