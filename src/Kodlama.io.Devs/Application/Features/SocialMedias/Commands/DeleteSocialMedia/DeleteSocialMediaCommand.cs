using Application.Features.SocialMedias.Dtos;
using Application.Features.SocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Commands.DeleteSocialMedia
{
    public class DeleteSocialMediaCommand : IRequest<DeletedSocialMediaDto>
    {
        public int Id { get; set; }

        public class DeleteSocialMediaCommandHandler : IRequestHandler<DeleteSocialMediaCommand, DeletedSocialMediaDto>
        {
            private readonly ISocialMediaRepository _socialMediaRepository;
            private readonly IMapper _mapper;
            private readonly SocialMediaBusinessRules _socialMediaBusinessRules;

            public DeleteSocialMediaCommandHandler(
                ISocialMediaRepository socialMediaRepository,
                IMapper mapper,
                SocialMediaBusinessRules socialMediaBusinessRules
            )
            {
                _socialMediaRepository = socialMediaRepository;
                _mapper = mapper;
                _socialMediaBusinessRules = socialMediaBusinessRules;
            }

            public async Task<DeletedSocialMediaDto> Handle(DeleteSocialMediaCommand request, CancellationToken cancellationToken)
            {
                SocialMedia? socialMedia = await _socialMediaRepository.GetAsync(s => s.Id == request.Id);

                _socialMediaBusinessRules.SocialMediaShouldExistWhenRequested(socialMedia);

                SocialMedia deletedSocialMedia = await _socialMediaRepository.DeleteAsync(socialMedia);
                DeletedSocialMediaDto deletedSocialMediaDto = _mapper.Map<DeletedSocialMediaDto>(deletedSocialMedia);

                return deletedSocialMediaDto;
            }
        }
    }
}
