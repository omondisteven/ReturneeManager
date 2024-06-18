using ReturneeManager.Application.Extensions;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Application.Specifications.Catalog;
using ReturneeManager.Domain.Entities.Catalog;
using ReturneeManager.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Features.Products.Queries.GetAllPaged
{
    public class GetAllProductsQuery : IRequest<PaginatedResult<GetAllPagedProductsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllProductsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResult<GetAllPagedProductsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedProductsResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetAllPagedProductsResponse>> expression = e => new GetAllPagedProductsResponse
            {
                Id = e.Id,
                Name = e.Name,
                //Description = e.Description,
                Rate = e.Rate,
                Barcode = e.Barcode,

                Brand = e.Brand.Name,
                BrandId = e.BrandId,

                //adding IdType
                IdType = e.IdType.Name,
                IdTypeId = e.IdTypeId,

                //adding Gender
                Gender = e.Gender.Name,
                GenderId = e.GenderId,

                //adding District
                District = e.District.Name,
                DistrictId = e.DistrictId,

                //adding Division
                Division = e.Division.Name,
                DivisionId = e.DivisionId,

                //adding Upazila
                Upazila = e.Upazila.Name,
                UpazilaId = e.UpazilaId,

                //adding Ward
                Ward = e.Ward.Name,
                WardId = e.WardId,

                //adding FromCountry
                FromCountry = e.FromCountry.Name,
                FromCountryId = e.FromCountryId,

                //Other non-lookup fiels
                IdNumber = e.IdNumber,
                DateOfBirth = e.DateOfBirth,
                MotherName = e.MotherName,
                FatherName = e.FatherName,
                MobileNumber = e.MobileNumber,
                HouseVillage = e.HouseVillage,
                StreetAddress = e.StreetAddress,
                PostCode = e.PostCode,
                ReturnReason = e.ReturnReason,
                ReturnDate = e.ReturnDate,
            };
            var productFilterSpec = new ProductFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Product>().Entities
                   .Specify(productFilterSpec)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Product>().Entities
                   .Specify(productFilterSpec)
                   .OrderBy(ordering) // require system.linq.dynamic.core
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;

            }
        }
    }
}