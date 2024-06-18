using AutoMapper;
using ReturneeManager.Infrastructure.Models.Audit;
using ReturneeManager.Application.Responses.Audit;

namespace ReturneeManager.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}