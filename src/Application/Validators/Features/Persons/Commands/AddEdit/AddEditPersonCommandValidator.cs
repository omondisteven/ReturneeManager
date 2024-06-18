using ReturneeManager.Application.Features.Persons.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ReturneeManager.Application.Validators.Features.Persons.Commands.AddEdit
{
    public class AddEditPersonCommandValidator : AbstractValidator<AddEditPersonCommand>
    {
        public AddEditPersonCommandValidator(IStringLocalizer<AddEditPersonCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.FatherName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Faher's name is required!"]);
            RuleFor(request => request.MotherName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Mother's name is required!"]);
            RuleFor(request => request.IdNumber)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["ID Number is required!"]);
            RuleFor(request => request.IdTypeId)
                .GreaterThan(0).WithMessage(x => localizer["Id Type is required!"]);
            RuleFor(request => request.GenderId)
                .GreaterThan(0).WithMessage(x => localizer["Gender is required"]);
            RuleFor(request => request.DivisionId)
                .GreaterThan(0).WithMessage(x => localizer["Division is required"]);
            RuleFor(request => request.DistrictId)
                .GreaterThan(0).WithMessage(x => localizer["District is required"]);
            RuleFor(request => request.UpazilaId)
                .GreaterThan(0).WithMessage(x => localizer["Upazila is required"]);
            RuleFor(request => request.WardId)
                .GreaterThan(0).WithMessage(x => localizer["Ward is required"]);
            RuleFor(request => request.DivisionId2)
                .GreaterThan(0).WithMessage(x => localizer["Division is required"]);
            RuleFor(request => request.DistrictId2)
                .GreaterThan(0).WithMessage(x => localizer["District is required"]);
            RuleFor(request => request.UpazilaId2)
                .GreaterThan(0).WithMessage(x => localizer["Upazila is required"]);
            RuleFor(request => request.WardId2)
                .GreaterThan(0).WithMessage(x => localizer["Ward is required"]);
            RuleFor(request => request.HouseVillage)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["House/Villagee is required!"]);
            RuleFor(request => request.StreetAddress)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Street Address is required!"]);
            RuleFor(request => request.PostCode)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Post Code is required!"]);
            RuleFor(request => request.PostCode2)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Post Code is required!"]);
            RuleFor(request => request.ReturnDocument)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Return Document is required!"]);
            RuleFor(request => request.ReturnReason)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Return Reason is required!"]);
            RuleFor(request => request.MobileNumber)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Mobile Numbere is required!"]);
            RuleFor(request => request.HouseVillage2)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["House/Village is required!"]);
            RuleFor(request => request.StreetAddress2)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Street Address is required!"]);
        }
    }
}