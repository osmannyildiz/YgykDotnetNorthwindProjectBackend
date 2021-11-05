using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Business/Abstract/IAuthService.cs
    public interface IAuthService {
        IDataResult<User> Register(UserRegisterDto userRegisterDto);
        IDataResult<User> Login(UserLoginDto userLoginDto);
        // FIXME Method name and purpose is confusing (it returns ErrorResult when a result is found)
        // Also shouldn't it be a part of IUserService rather than here? Or maybe as a CheckIf method?
        IResult UserWithEmailAlreadyExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
