using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ReturneeManager.Shared.Constants.Application;

namespace ReturneeManager.Application.Features.Districts.Commands.Delete
{
    public class DeleteDistrictCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand, Result<int>>
    {
        //private readonly IPersonRepository _personRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer<DeleteDistrictCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteDistrictCommandHandler(IUnitOfWork<int> unitOfWork, IPersonRepository personRepository, IStringLocalizer<DeleteDistrictCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteDistrictCommand command, CancellationToken cancellationToken)
        {
            var isDistrictUsed = await _personRepository.IsDistrictUsed(command.Id);
            if (!isDistrictUsed)
            {
                var district = await _unitOfWork.Repository<District>().GetByIdAsync(command.Id);
                if (district != null)
                {
                    await _unitOfWork.Repository<District>().DeleteAsync(district);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDistrictsCacheKey);
                    return await Result<int>.SuccessAsync(district.Id, _localizer["District Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["District Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}