using AutoMapper;
using ReturneeManager.Application.Features.Brands.Commands.AddEdit;
using ReturneeManager.Application.Features.Brands.Queries.GetAll;
using ReturneeManager.Application.Features.Brands.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}