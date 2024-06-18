using AutoMapper;
using ReturneeManager.Application.Features.FromCountries.Commands.AddEdit;
using ReturneeManager.Application.Features.FromCountries.Queries.GetAll;
using ReturneeManager.Application.Features.FromCountries.Queries.GetById;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class FromCountryProfile : Profile
    {
        public FromCountryProfile()
        {
            CreateMap<AddEditFromCountryCommand, FromCountry>().ReverseMap();
            CreateMap<GetFromCountryByIdResponse, FromCountry>().ReverseMap();
            CreateMap<GetAllFromCountriesResponse, FromCountry>().ReverseMap();
        }
    }
}
