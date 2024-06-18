using AutoMapper;
using ReturneeManager.Infrastructure.Models.Identity;
using ReturneeManager.Application.Responses.Identity;

namespace ReturneeManager.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}