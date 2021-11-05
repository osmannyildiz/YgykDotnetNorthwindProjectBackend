using Business.Abstract;
using Core.Entities.Dtos;
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
            var userToLogin = _authService.Login(userLoginDto);
            if (!userToLogin.Success) {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success) {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto) {
            var userExists = _authService.UserWithEmailAlreadyExists(userRegisterDto.Email);
            if (!userExists.Success) {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (!result.Success) {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
