using Application.Features.Authentications.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentications.Rules
{
    public class AuthenticationBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(u => u.Email == email);
            if (result.Items.Any())
                throw new BusinessException(Messages.UserEmailExists);
        }

        public void UserShouldExistWhenRequested(User user)
        {
            if (user is null)
                throw new BusinessException(Messages.RequestedUserDoesNotExist);
        }

        public void UserCredentialsMustMatch(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            bool result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result)
                throw new BusinessException(Messages.PasswordIncorrect);
        }
    }
}
