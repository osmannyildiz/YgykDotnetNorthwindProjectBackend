using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Business/Concrete/AuthManager.cs
    public class AuthManager : IAuthService {
        private IUserService _userManager;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userManager, ITokenHelper tokenHelper) {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
        }

        // TODO Password constraints (min length etc.)
        public IDataResult<User> Register(UserRegisterDto userRegisterDto) {
            byte[] passwordHash, passwordSalt;
            HashingTool.HashPassword(userRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userManager.Add(user);
            return new SuccessDataResult<User>(user, Messages.RegisterSuccessful);
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto) {
            var userToCheck = _userManager.GetByEmail(userLoginDto.Email).Data;
            if (userToCheck == null) {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingTool.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt)) {
                return new ErrorDataResult<User>(Messages.WrongPassword);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.LoginSuccessful);
        }

        public IResult UserWithEmailAlreadyExists(string email) {
            if (_userManager.GetByEmail(email).Data != null) {
                return new ErrorResult(Messages.UserWithEmailAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user) {
            var claims = _userManager.GetOperationClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
