using AutoMapper;
using ReturneeManager.Application.Interfaces.Chat;
using ReturneeManager.Application.Models.Chat;
using ReturneeManager.Infrastructure.Models.Identity;

namespace ReturneeManager.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}