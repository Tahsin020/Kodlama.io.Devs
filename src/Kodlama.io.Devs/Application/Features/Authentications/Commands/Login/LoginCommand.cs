using Application.Features.Authentications.Dtos;
using Application.Features.Authentications.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentications.Commands.Login
{
    public class LoginCommand : IRequest<AccessTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessTokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly AuthenticationBusinessRules _authenticationBusinessRules;
            private readonly ITokenHelper _tokenHelper;

            public LoginCommandHandler(
                IUserRepository userRepository,
                IMapper mapper,
                AuthenticationBusinessRules authenticationBusinessRules,
                ITokenHelper tokenHelper
            )
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _authenticationBusinessRules = authenticationBusinessRules;
                _tokenHelper = tokenHelper;
            }

            public async Task<AccessTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(
                    u => u.Email == request.Email,
                    include: u => u.Include(c => c.UserOperationClaims).ThenInclude(c => c.OperationClaim)
                );

                _authenticationBusinessRules.UserShouldExistWhenRequested(user);
                _authenticationBusinessRules.UserCredentialsMustMatch(request.Password, user.PasswordHash, user.PasswordSalt);

                List<OperationClaim> operationClaims = new();

                foreach (UserOperationClaim userOperationClaim in user.UserOperationClaims)
                    operationClaims.Add(userOperationClaim.OperationClaim);

                AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
                AccessTokenDto accessTokenDto = _mapper.Map<AccessTokenDto>(accessToken);

                return accessTokenDto;
            }
        }
    }
}
