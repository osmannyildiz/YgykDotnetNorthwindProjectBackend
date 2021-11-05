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
        private IAuthService _authManager;

        public AuthController(IAuthService authManager) {
            _authManager = authManager;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto) {
            var userToLogin = _authManager.Login(userLoginDto);
            if (!userToLogin.Success) {
                return BadRequest(userToLogin.Message);
            }

            var result = _authManager.CreateAccessToken(userToLogin.Data);
            if (!result.Success) {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto) {
            var userExists = _authManager.UserWithEmailAlreadyExists(userRegisterDto.Email);
            if (!userExists.Success) {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authManager.Register(userRegisterDto);
            var result = _authManager.CreateAccessToken(registerResult.Data);
            if (!result.Success) {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
