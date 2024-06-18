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

namespace ReturneeManager.Application.Features.FromCountries.Commands.AddEdit
{
    public partial class AddEditFromCountryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }

    internal class AddEditFromCountryCommandHandler : IRequestHandler<AddEditFromCountryCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditFromCountryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditFromCountryCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditFromCountryCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditFromCountryCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var fromCountry = _mapper.Map<FromCountry>(command);
                await _unitOfWork.Repository<FromCountry>().AddAsync(fromCountry);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllFromCountriesCacheKey);
                return await Result<int>.SuccessAsync(fromCountry.Id, _localizer["Id Type Saved"]);
            }
            else
            {
                var fromCountry = await _unitOfWork.Repository<FromCountry>().GetByIdAsync(command.Id);
                if (fromCountry != null)
                {
                    fromCountry.Name = command.Name ?? fromCountry.Name;
                    fromCountry.Description = command.Description ?? fromCountry.Description;

                    await _unitOfWork.Repository<FromCountry>().UpdateAsync(fromCountry);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllFromCountriesCacheKey);
                    return await Result<int>.SuccessAsync(fromCountry.Id, _localizer["Id Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Id Type Not Found!"]);
                }
            }
        }
    }
}
