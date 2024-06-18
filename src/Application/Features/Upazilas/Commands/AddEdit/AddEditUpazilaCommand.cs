using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using ReturneeManager.Shared.Constants.Application;

namespace ReturneeManager.Application.Features.Upazilas.Commands.AddEdit
{
    public partial class AddEditUpazilaCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    internal class AddEditUpazilaCommandHandler : IRequestHandler<AddEditUpazilaCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditUpazilaCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditUpazilaCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditUpazilaCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditUpazilaCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var upazila = _mapper.Map<Upazila>(command);
                await _unitOfWork.Repository<Upazila>().AddAsync(upazila);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllUpazilasCacheKey);
                return await Result<int>.SuccessAsync(upazila.Id, _localizer["Id Type Saved"]);
            }
            else
            {
                var upazila = await _unitOfWork.Repository<Upazila>().GetByIdAsync(command.Id);
                if (upazila != null)
                {
                    upazila.Name = command.Name ?? upazila.Name;
                    upazila.Description = command.Description ?? upazila.Description;

                    await _unitOfWork.Repository<Upazila>().UpdateAsync(upazila);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllUpazilasCacheKey);
                    return await Result<int>.SuccessAsync(upazila.Id, _localizer["Id Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Id Type Not Found!"]);
                }
            }
        }
    }
}
