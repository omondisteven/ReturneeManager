using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ReturneeManager.Shared.Constants.Application;

namespace ReturneeManager.Application.Features.IdTypes.Commands.Delete
{
    public class DeleteIdTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteIdTypeCommandHandler : IRequestHandler<DeleteIdTypeCommand, Result<int>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer<DeleteIdTypeCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteIdTypeCommandHandler(IUnitOfWork<int> unitOfWork, IPersonRepository personRepository, IStringLocalizer<DeleteIdTypeCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteIdTypeCommand command, CancellationToken cancellationToken)
        {
            var isIdTypeUsed = await _personRepository.IsIdTypeUsed(command.Id);
            if (!isIdTypeUsed)
            {
                var idType = await _unitOfWork.Repository<IdType>().GetByIdAsync(command.Id);
                if (idType != null)
                {
                    await _unitOfWork.Repository<IdType>().DeleteAsync(idType);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllIdTypesCacheKey);
                    return await Result<int>.SuccessAsync(idType.Id, _localizer["IdType Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["IdType Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}