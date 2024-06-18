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

namespace ReturneeManager.Application.Features.IdTypes.Commands.AddEdit
{
    public partial class AddEditIdTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    internal class AddEditIdTypeCommandHandler : IRequestHandler<AddEditIdTypeCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditIdTypeCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditIdTypeCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditIdTypeCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditIdTypeCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var idType = _mapper.Map<IdType>(command);
                await _unitOfWork.Repository<IdType>().AddAsync(idType);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllIdTypesCacheKey);
                return await Result<int>.SuccessAsync(idType.Id, _localizer["Id Type Saved"]);
            }
            else
            {
                var idType = await _unitOfWork.Repository<IdType>().GetByIdAsync(command.Id);
                if (idType != null)
                {
                    idType.Name = command.Name ?? idType.Name;
                    idType.Description = command.Description ?? idType.Description;

                    await _unitOfWork.Repository<IdType>().UpdateAsync(idType);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllIdTypesCacheKey);
                    return await Result<int>.SuccessAsync(idType.Id, _localizer["Id Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Id Type Not Found!"]);
                }
            }
        }
    }
}
