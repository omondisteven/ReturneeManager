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

namespace ReturneeManager.Application.Features.Wards.Commands.AddEdit
{
    public partial class AddEditWardCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    internal class AddEditWardCommandHandler : IRequestHandler<AddEditWardCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditWardCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditWardCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditWardCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditWardCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var ward = _mapper.Map<Ward>(command);
                await _unitOfWork.Repository<Ward>().AddAsync(ward);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllWardsCacheKey);
                return await Result<int>.SuccessAsync(ward.Id, _localizer["Id Type Saved"]);
            }
            else
            {
                var ward = await _unitOfWork.Repository<Ward>().GetByIdAsync(command.Id);
                if (ward != null)
                {
                    ward.Name = command.Name ?? ward.Name;
                    ward.Description = command.Description ?? ward.Description;

                    await _unitOfWork.Repository<Ward>().UpdateAsync(ward);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllWardsCacheKey);
                    return await Result<int>.SuccessAsync(ward.Id, _localizer["Id Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Id Type Not Found!"]);
                }
            }
        }
    }
}
