using AutoMapper;
using ReturneeManager.Infrastructure.Models.Identity;
using ReturneeManager.Application.Responses.Identity;

namespace ReturneeManager.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, BlazorHeroUser>().ReverseMap();
            CreateMap<ChatUserResponse, BlazorHeroUser>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}