using AutoMapper;
using ReturneeManager.Application.Features.Wards.Commands.AddEdit;
using ReturneeManager.Application.Features.Wards.Queries.GetAll;
using ReturneeManager.Application.Features.Wards.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class WardProfile : Profile
    {
        public WardProfile()
        {
            CreateMap<AddEditWardCommand, Ward>().ReverseMap();
            CreateMap<GetWardByIdResponse, Ward>().ReverseMap();
            CreateMap<GetAllWardsResponse, Ward>().ReverseMap();
        }
    }
}
