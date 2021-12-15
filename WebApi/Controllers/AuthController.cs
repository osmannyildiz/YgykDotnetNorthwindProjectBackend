using Business.Abstract;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/WebAPI/Controllers/AuthController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto) {
            var loginResult = _authService.Login(userLoginDto);
            if (!loginResult.Success) {
                return BadRequest(new ErrorResult(loginResult.Message));
            }

            var tokenResult = _authService.CreateAccessToken(loginResult.Data);
            if (!tokenResult.Success) {
                return BadRequest(new ErrorResult(tokenResult.Message));
            }

            return Ok(new SuccessDataResult<AccessToken>(tokenResult.Data, loginResult.Message));
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto) {
            var userExistsResult = _authService.UserWithEmailAlreadyExists(userRegisterDto.Email);
            if (!userExistsResult.Success) {
                return BadRequest(new ErrorResult(userExistsResult.Message));
            }

            var registerResult = _authService.Register(userRegisterDto);
            if (!registerResult.Success) {
                return BadRequest(new ErrorResult(registerResult.Message));
            }

            var tokenResult = _authService.CreateAccessToken(registerResult.Data);
            if (!tokenResult.Success) {
                return BadRequest(new ErrorResult(tokenResult.Message));
            }

            return Ok(new SuccessDataResult<AccessToken>(tokenResult.Data, registerResult.Message));
        }
    }
}
