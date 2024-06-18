using AutoMapper;
using ReturneeManager.Application.Features.Genders.Commands.AddEdit;
using ReturneeManager.Application.Features.Genders.Queries.GetAll;
using ReturneeManager.Application.Features.Genders.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<AddEditGenderCommand, Gender>().ReverseMap();
            CreateMap<GetGenderByIdResponse, Gender>().ReverseMap();
            CreateMap<GetAllGendersResponse, Gender>().ReverseMap();
        }
    }
}
