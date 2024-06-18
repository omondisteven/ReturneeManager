using AutoMapper;
using ReturneeManager.Application.Features.Upazilas.Commands.AddEdit;
using ReturneeManager.Application.Features.Upazilas.Queries.GetAll;
using ReturneeManager.Application.Features.Upazilas.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class UpazilaProfile : Profile
    {
        public UpazilaProfile()
        {
            CreateMap<AddEditUpazilaCommand, Upazila>().ReverseMap();
            CreateMap<GetUpazilaByIdResponse, Upazila>().ReverseMap();
            CreateMap<GetAllUpazilasResponse, Upazila>().ReverseMap();
        }
    }
}
