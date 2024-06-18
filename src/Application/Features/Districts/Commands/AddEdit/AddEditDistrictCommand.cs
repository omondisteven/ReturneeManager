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

namespace ReturneeManager.Application.Features.Districts.Commands.AddEdit
{
    public partial class AddEditDistrictCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    internal class AddEditDistrictCommandHandler : IRequestHandler<AddEditDistrictCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditDistrictCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditDistrictCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditDistrictCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditDistrictCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var district = _mapper.Map<District>(command);
                await _unitOfWork.Repository<District>().AddAsync(district);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDistrictsCacheKey);
                return await Result<int>.SuccessAsync(district.Id, _localizer["Id Type Saved"]);
            }
            else
            {
                var district = await _unitOfWork.Repository<District>().GetByIdAsync(command.Id);
                if (district != null)
                {
                    district.Name = command.Name ?? district.Name;
                    district.Description = command.Description ?? district.Description;

                    await _unitOfWork.Repository<District>().UpdateAsync(district);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDistrictsCacheKey);
                    return await Result<int>.SuccessAsync(district.Id, _localizer["Id Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Id Type Not Found!"]);
                }
            }
        }
    }
}
