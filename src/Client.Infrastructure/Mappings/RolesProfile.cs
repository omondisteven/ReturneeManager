using AutoMapper;
using ReturneeManager.Application.Requests.Identity;
using ReturneeManager.Application.Responses.Identity;

namespace ReturneeManager.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}