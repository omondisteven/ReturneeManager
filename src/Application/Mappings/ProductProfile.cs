using AutoMapper;
using ReturneeManager.Application.Features.Products.Commands.AddEdit;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}