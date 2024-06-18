using AutoMapper;
using ReturneeManager.Application.Features.Persons.Commands.AddEdit;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<AddEditPersonCommand, Person>().ReverseMap();
        }
    }
}