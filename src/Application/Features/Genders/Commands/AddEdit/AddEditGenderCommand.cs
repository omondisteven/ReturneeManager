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

namespace ReturneeManager.Application.Features.Genders.Commands.AddEdit
{
    public partial class AddEditGenderCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    internal class AddEditGenderCommandHandler : IRequestHandler<AddEditGenderCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditGenderCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditGenderCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditGenderCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditGenderCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var gender = _mapper.Map<Gender>(command);
                await _unitOfWork.Repository<Gender>().AddAsync(gender);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllGendersCacheKey);
                return await Result<int>.SuccessAsync(gender.Id, _localizer["Id Type Saved"]);
            }
            else
            {
                var gender = await _unitOfWork.Repository<Gender>().GetByIdAsync(command.Id);
                if (gender != null)
                {
                    gender.Name = command.Name ?? gender.Name;
                    gender.Description = command.Description ?? gender.Description;

                    await _unitOfWork.Repository<Gender>().UpdateAsync(gender);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllGendersCacheKey);
                    return await Result<int>.SuccessAsync(gender.Id, _localizer["Id Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Id Type Not Found!"]);
                }
            }
        }
    }
}
