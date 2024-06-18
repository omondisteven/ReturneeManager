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

namespace ReturneeManager.Application.Features.Divisions.Commands.AddEdit
{
    public partial class AddEditDivisionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    internal class AddEditDivisionCommandHandler : IRequestHandler<AddEditDivisionCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditDivisionCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditDivisionCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditDivisionCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditDivisionCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var division = _mapper.Map<Division>(command);
                await _unitOfWork.Repository<Division>().AddAsync(division);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDivisionsCacheKey);
                return await Result<int>.SuccessAsync(division.Id, _localizer["Id Type Saved"]);
            }
            else
            {
                var division = await _unitOfWork.Repository<Division>().GetByIdAsync(command.Id);
                if (division != null)
                {
                    division.Name = command.Name ?? division.Name;
                    division.Description = command.Description ?? division.Description;

                    await _unitOfWork.Repository<Division>().UpdateAsync(division);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllDivisionsCacheKey);
                    return await Result<int>.SuccessAsync(division.Id, _localizer["Id Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Id Type Not Found!"]);
                }
            }
        }
    }
}
