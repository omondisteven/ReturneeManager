using AutoMapper;
using ReturneeManager.Application.Features.Divisions.Commands.AddEdit;
using ReturneeManager.Application.Features.Divisions.Queries.GetAll;
using ReturneeManager.Application.Features.Divisions.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class DivisionProfile : Profile
    {
        public DivisionProfile()
        {
            CreateMap<AddEditDivisionCommand, Division>().ReverseMap();
            CreateMap<GetDivisionByIdResponse, Division>().ReverseMap();
            CreateMap<GetAllDivisionsResponse, Division>().ReverseMap();
        }
    }
}
