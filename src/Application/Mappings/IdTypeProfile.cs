using AutoMapper;
using ReturneeManager.Application.Features.IdTypes.Commands.AddEdit;
using ReturneeManager.Application.Features.IdTypes.Queries.GetAll;
using ReturneeManager.Application.Features.IdTypes.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class IdTypeProfile : Profile
    {
        public IdTypeProfile()
        {
            CreateMap<AddEditIdTypeCommand, IdType>().ReverseMap();
            CreateMap<GetIdTypeByIdResponse, IdType>().ReverseMap();
            CreateMap<GetAllIdTypesResponse, IdType>().ReverseMap();
        }
    }
}
