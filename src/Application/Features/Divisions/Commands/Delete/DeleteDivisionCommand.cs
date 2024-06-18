using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ReturneeManager.Shared.Constants.Application;

namespace ReturneeManager.Application.Features.Divisions.Commands.Delete
{
    public class DeleteDivisionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteDivisionCommandHandler : IRequestHandler<DeleteDivisionCommand, Result<int>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer<DeleteDivisionCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteDivisionCommandHandler(IUnitOfWork<int> unitOfWork, IPersonRepository personRepository, IStringLocalizer<DeleteDivisionCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteDivisionCommand command, CancellationToken cancellationToken)
        {
            var isDivisionUsed = await _personRepository.IsDivisionUsed(command.Id);
            if (!isDivisionUsed)
            {
                var division = await _unitOfWork.Repository<Division>().GetByIdAsync(command.Id);
                if (division != null)
                {
                    await _unitOfWork.Repository<Division>().DeleteAsync(division);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDivisionsCacheKey);
                    return await Result<int>.SuccessAsync(division.Id, _localizer["Division Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Division Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}