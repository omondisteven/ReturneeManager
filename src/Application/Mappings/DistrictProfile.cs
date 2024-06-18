using AutoMapper;
using ReturneeManager.Application.Features.Districts.Commands.AddEdit;
using ReturneeManager.Application.Features.Districts.Queries.GetAll;
using ReturneeManager.Application.Features.Districts.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            CreateMap<AddEditDistrictCommand, District>().ReverseMap();
            CreateMap<GetDistrictByIdResponse, District>().ReverseMap();
            CreateMap<GetAllDistrictsResponse, District>().ReverseMap();
        }
    }
}
