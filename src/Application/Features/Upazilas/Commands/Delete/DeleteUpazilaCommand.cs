using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ReturneeManager.Shared.Constants.Application;

namespace ReturneeManager.Application.Features.Upazilas.Commands.Delete
{
    public class DeleteUpazilaCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteUpazilaCommandHandler : IRequestHandler<DeleteUpazilaCommand, Result<int>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer<DeleteUpazilaCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteUpazilaCommandHandler(IUnitOfWork<int> unitOfWork, IPersonRepository personRepository, IStringLocalizer<DeleteUpazilaCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteUpazilaCommand command, CancellationToken cancellationToken)
        {
            var isUpazilaUsed = await _personRepository.IsUpazilaUsed(command.Id);
            if (!isUpazilaUsed)
            {
                var upazila = await _unitOfWork.Repository<Upazila>().GetByIdAsync(command.Id);
                if (upazila != null)
                {
                    await _unitOfWork.Repository<Upazila>().DeleteAsync(upazila);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllUpazilasCacheKey);
                    return await Result<int>.SuccessAsync(upazila.Id, _localizer["Upazila Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Upazila Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}