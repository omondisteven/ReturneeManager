﻿using AutoMapper;
using ReturneeManager.Application.Requests.Identity;
using ReturneeManager.Application.Responses.Identity;
using ReturneeManager.Infrastructure.Models.Identity;

namespace ReturneeManager.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, BlazorHeroRoleClaim>()
                .ForMember(nameof(BlazorHeroRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(BlazorHeroRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, BlazorHeroRoleClaim>()
                .ForMember(nameof(BlazorHeroRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(BlazorHeroRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}