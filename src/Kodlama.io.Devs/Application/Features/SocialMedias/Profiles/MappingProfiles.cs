using Application.Features.SocialMedias.Commands.CreateSocialMedia;
using Application.Features.SocialMedias.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SocialMedia, CreatedSocialMediaDto>()
                .ForMember(
                    c => c.SocialMediaType,
                    opt => opt.MapFrom(c => Enum.GetName(typeof(SocialMediaType), c.SocialMediaType))
                )
                .ReverseMap();
            CreateMap<SocialMedia, CreateSocialMediaCommand>().ReverseMap();
            CreateMap<SocialMedia, UpdatedSocialMediaDto>()
                .ForMember(
                    c => c.SocialMediaType,
                    opt => opt.MapFrom(c => Enum.GetName(typeof(SocialMediaType), c.SocialMediaType))
                )
                .ReverseMap();
            CreateMap<SocialMedia, DeletedSocialMediaDto>()
                .ForMember(
                    c => c.SocialMediaType,
                    opt => opt.MapFrom(c => Enum.GetName(typeof(SocialMediaType), c.SocialMediaType))
                )
                .ReverseMap();
        }
    }
}
