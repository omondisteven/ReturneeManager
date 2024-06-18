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

namespace ReturneeManager.Application.Features.Persons.Queries.GetAllPaged
{
    public class GetAllPersonsQuery : IRequest<PaginatedResult<GetAllPagedPersonsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllPersonsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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

    internal class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, PaginatedResult<GetAllPagedPersonsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPersonsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedPersonsResponse>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Person, GetAllPagedPersonsResponse>> expression = e => new GetAllPagedPersonsResponse
            {
                Id = e.Id,
                Name = e.Name,                

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

                //Permanet Address
                SameAddress = e.SameAddress,
                HouseVillage2 = e.HouseVillage2,
                StreetAddress2 = e.StreetAddress2,
                PostCode2 = e.PostCode2,

                //adding District
                DistrictId2 = e.DistrictId,

                //adding Division
                DivisionId2 = e.DivisionId,

                //adding Upazila
                UpazilaId2 = e.UpazilaId,

                //adding Ward
                WardId2 = e.WardId,
            };
            var personFilterSpec = new PersonFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Person>().Entities
                   .Specify(personFilterSpec)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Person>().Entities
                   .Specify(personFilterSpec)
                   .OrderBy(ordering) // require system.linq.dynamic.core
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;

            }
        }
    }
}