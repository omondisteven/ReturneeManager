using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ReturneeManager.Shared.Constants.Application;

namespace ReturneeManager.Application.Features.Wards.Commands.Delete
{
    public class DeleteWardCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteWardCommandHandler : IRequestHandler<DeleteWardCommand, Result<int>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer<DeleteWardCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteWardCommandHandler(IUnitOfWork<int> unitOfWork, IPersonRepository personRepository, IStringLocalizer<DeleteWardCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteWardCommand command, CancellationToken cancellationToken)
        {
            var isWardUsed = await _personRepository.IsWardUsed(command.Id);
            if (!isWardUsed)
            {
                var ward = await _unitOfWork.Repository<Ward>().GetByIdAsync(command.Id);
                if (ward != null)
                {
                    await _unitOfWork.Repository<Ward>().DeleteAsync(ward);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllWardsCacheKey);
                    return await Result<int>.SuccessAsync(ward.Id, _localizer["Ward Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Ward Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}