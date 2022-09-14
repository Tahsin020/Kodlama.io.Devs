using Application.Features.SocialMedias.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Rules
{
    public class SocialMediaBusinessRules
    {
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IUserRepository _userRepository;

        public SocialMediaBusinessRules(ISocialMediaRepository socialMediaRepository, IUserRepository userRepository)
        {
            _socialMediaRepository = socialMediaRepository;
            _userRepository = userRepository;
        }

        public async Task SocialMediaCanNotBeDuplicatedWhenInserted(SocialMediaType socialMediaType, int userId)
        {
            IPaginate<SocialMedia> result = await _socialMediaRepository.GetListAsync(
                p => p.UserId == userId && p.SocialMediaType == socialMediaType
            );
            if (result.Items.Any())
                throw new BusinessException(Messages.SocialMediaExists);
        }

        public async Task UserShouldExistWhenInserted(int userId)
        {
            User? user = await _userRepository.GetAsync(p => p.Id == userId);
            if (user is null)
                throw new BusinessException(Messages.UserDoesNotExist);
        }

        public void SocialMediaShouldExistWhenRequested(SocialMedia socialMedia)
        {
            if (socialMedia is null)
                throw new BusinessException(Messages.RequestedSocialMediaDoesNotExist);
        }
    }
}
