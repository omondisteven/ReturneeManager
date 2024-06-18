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

namespace ReturneeManager.Application.Features.Products.Commands.AddEdit
{
    public partial class AddEditProductCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        //public string Description { get; set; }
        public string ImageDataURL { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public int BrandId { get; set; }
        public int IdTypeId { get; set; }
        public string IdNumber { get; set; }
        public int GenderId { get; set; }
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public int UpazilaId { get; set; }
        public int WardId { get; set; }
        public int FromCountryId { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string MobileNumber { get; set; }
        public string HouseVillage { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string ReturnReason { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnDocument { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditProductCommandHandler> _localizer;

        public AddEditProductCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditProductCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditProductCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Product>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.Barcode == command.Barcode, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["Barcode already exists."]);
            }

            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.Barcode}{uploadRequest.Extension}";
            }

            if (command.Id == 0)
            {
                var product = _mapper.Map<Product>(command);
                if (uploadRequest != null)
                {
                    product.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.Repository<Product>().AddAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(product.Id, _localizer["Product Saved"]);
            }
            else
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);
                if (product != null)
                {
                    product.Name = command.Name ?? product.Name;
                    //product.Description = command.Description ?? product.Description;
                    if (uploadRequest != null)
                    {
                        product.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                    }
                    product.Rate = (command.Rate == 0) ? product.Rate : command.Rate;
                    product.BrandId = (command.BrandId == 0) ? product.BrandId : command.BrandId;
                    product.IdTypeId = (command.IdTypeId == 0) ? product.IdTypeId : command.IdTypeId;
                    product.IdNumber = command.IdNumber ?? product.IdNumber;
                    product.GenderId = (command.GenderId == 0) ? product.GenderId : command.GenderId;
                    product.DistrictId = (command.DistrictId == 0) ? product.DistrictId : command.DistrictId;
                    product.DivisionId = (command.DivisionId == 0) ? product.DivisionId : command.DivisionId;
                    product.FromCountryId = (command.FromCountryId == 0) ? product.FromCountryId : command.FromCountryId;
                    product.UpazilaId = (command.UpazilaId == 0) ? product.UpazilaId : command.UpazilaId;
                    product.WardId = (command.WardId == 0) ? product.WardId : command.WardId;

                    //Other fields
                    if (command.DateOfBirth != DateTime.MinValue)
                    {
                        product.DateOfBirth = command.DateOfBirth;
                    }
                    product.MotherName = command.MotherName ?? product.MotherName;
                    product.FatherName = command.FatherName ?? product.FatherName;
                    product.MobileNumber = command.MobileNumber ?? product.MobileNumber;
                    product.HouseVillage = command.HouseVillage ?? product.HouseVillage;
                    product.StreetAddress = command.StreetAddress ?? product.StreetAddress;
                    product.PostCode = command.PostCode ?? product.PostCode;
                    product.ReturnReason = command.ReturnReason ?? product.ReturnReason;
                    if (command.ReturnDate != DateTime.MinValue)
                    {
                        product.ReturnDate = command.ReturnDate;
                    }
                    product.ReturnDocument = command.ReturnDocument ?? product.ReturnDocument;
                    await _unitOfWork.Repository<Product>().UpdateAsync(product);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(product.Id, _localizer["Product Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Product Not Found!"]);
                }
            }
        }
    }
}