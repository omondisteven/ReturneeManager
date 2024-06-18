using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Application.Interfaces.Services;
using ReturneeManager.Application.Requests;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReturneeManager.Application.Features.Persons.Commands.AddEdit
{
    public partial class AddEditPersonCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageDataURL { get; set; }
        [Required]
        public int IdTypeId { get; set; }
        [Required]
        public string IdNumber { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        [Required]
        public int UpazilaId { get; set; }
        [Required]
        public int WardId { get; set; }
        [Required]
        public int FromCountryId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string HouseVillage { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string ReturnReason { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        public string ReturnDocument { get; set; }
        public UploadRequest UploadRequest { get; set; }

        public bool SameAddress { get; set; }
        [Required]
        public string HouseVillage2 { get; set; }
        [Required]
        public string StreetAddress2 { get; set; }
        [Required]
        public int DivisionId2 { get; set; }
        [Required]
        public int DistrictId2 { get; set; }
        [Required]
        public int UpazilaId2 { get; set; }
        [Required]
        public int WardId2 { get; set; }
        [Required]
        public string PostCode2 { get; set; }
    }

    internal class AddEditPersonCommandHandler : IRequestHandler<AddEditPersonCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditPersonCommandHandler> _localizer;

        public AddEditPersonCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditPersonCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditPersonCommand command, CancellationToken cancellationToken)
        {
            var uploadRequest = command.UploadRequest;

            if (command.Id == 0)
            {
                var person = _mapper.Map<Person>(command);
                if (uploadRequest != null)
                {
                    person.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                }

                if (command.SameAddress)
                {
                    person.HouseVillage2 = person.HouseVillage;
                    person.StreetAddress2 = person.StreetAddress;
                    person.DivisionId2 = person.DivisionId;
                    person.DistrictId2 = person.DistrictId;
                    person.UpazilaId2 = person.UpazilaId;
                    person.WardId2 = person.WardId;
                    person.PostCode2 = person.PostCode;
                }
                else
                {
                    person.HouseVillage2 = "";
                    person.StreetAddress2 = "";
                    person.DivisionId2 = 0;
                    person.DistrictId2 = 0;
                    person.UpazilaId2 = 0;
                    person.WardId2 = 0;
                    person.PostCode2 = "";
                }

                person.SameAddress = command.SameAddress; // Store SameAddress value

                await _unitOfWork.Repository<Person>().AddAsync(person);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(person.Id, _localizer["Person Saved"]);
            }
            else
            {
                var person = await _unitOfWork.Repository<Person>().GetByIdAsync(command.Id);
                if (person != null)
                {
                    person.Name = command.Name ?? person.Name;
                    if (uploadRequest != null)
                    {
                        person.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                    }
                    person.IdTypeId = (command.IdTypeId == 0) ? person.IdTypeId : command.IdTypeId;
                    person.IdNumber = command.IdNumber ?? person.IdNumber;
                    person.GenderId = (command.GenderId == 0) ? person.GenderId : command.GenderId;
                    person.DistrictId = (command.DistrictId == 0) ? person.DistrictId : command.DistrictId;
                    person.DivisionId = (command.DivisionId == 0) ? person.DivisionId : command.DivisionId;
                    person.FromCountryId = (command.FromCountryId == 0) ? person.FromCountryId : command.FromCountryId;
                    person.UpazilaId = (command.UpazilaId == 0) ? person.UpazilaId : command.UpazilaId;
                    person.WardId = (command.WardId == 0) ? person.WardId : command.WardId;

                    if (command.DateOfBirth != DateTime.MinValue)
                    {
                        person.DateOfBirth = command.DateOfBirth;
                    }
                    person.MotherName = command.MotherName ?? person.MotherName;
                    person.FatherName = command.FatherName ?? person.FatherName;
                    person.MobileNumber = command.MobileNumber ?? person.MobileNumber;
                    person.HouseVillage = command.HouseVillage ?? person.HouseVillage;
                    person.StreetAddress = command.StreetAddress ?? person.StreetAddress;
                    person.PostCode = command.PostCode ?? person.PostCode;
                    person.ReturnReason = command.ReturnReason ?? person.ReturnReason;
                    if (command.ReturnDate != DateTime.MinValue)
                    {
                        person.ReturnDate = command.ReturnDate;
                    }
                    person.ReturnDocument = command.ReturnDocument ?? person.ReturnDocument;

                    person.HouseVillage2 = command.HouseVillage2 ?? person.HouseVillage2;
                    person.StreetAddress2 = command.StreetAddress2 ?? person.StreetAddress2;
                    person.DistrictId2 = (command.DistrictId2 == 0) ? person.DistrictId2 : command.DistrictId2;
                    person.DivisionId2 = (command.DivisionId2 == 0) ? person.DivisionId2 : command.DivisionId2;
                    person.UpazilaId2 = (command.UpazilaId2 == 0) ? person.UpazilaId2 : command.UpazilaId2;
                    person.WardId2 = (command.WardId2 == 0) ? person.WardId2 : command.WardId2;
                    person.PostCode2 = command.PostCode2 ?? person.PostCode2;

                    if (command.SameAddress)
                    {
                        person.HouseVillage2 = person.HouseVillage;
                        person.StreetAddress2 = person.StreetAddress;
                        person.DivisionId2 = person.DivisionId;
                        person.DistrictId2 = person.DistrictId;
                        person.UpazilaId2 = person.UpazilaId;
                        person.WardId2 = person.WardId;
                        person.PostCode2 = person.PostCode;
                    }

                    person.SameAddress = command.SameAddress; // Store SameAddress value

                    await _unitOfWork.Repository<Person>().UpdateAsync(person);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(person.Id, _localizer["Person Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Person Not Found!"]);
                }
            }
        }
    }

}