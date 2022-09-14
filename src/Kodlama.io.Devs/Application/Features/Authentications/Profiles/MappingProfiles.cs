using Application.Features.Authentications.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentications.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, CreatedUserDto>().ReverseMap();
            CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
        }
    }
}
