using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private ICurrentUserService _currentUserService;
        private ITokenHelper _tokenHelper;

        public AuthManager(ICurrentUserService currentUserService, ITokenHelper tokenHelper)
        {
            _currentUserService = currentUserService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<CurrentUser> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new CurrentUser
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _currentUserService.Add(user);
            return new SuccessDataResult<CurrentUser>(user, Messages.UserRegistered);
        }

        public IDataResult<CurrentUser> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _currentUserService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<CurrentUser>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<CurrentUser>(Messages.PasswordError);
            }

            return new SuccessDataResult<CurrentUser>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_currentUserService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(CurrentUser currentUser)
        {
            var claims = _currentUserService.GetClaims(currentUser);
            var accessToken = _tokenHelper.CreateToken(currentUser, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
