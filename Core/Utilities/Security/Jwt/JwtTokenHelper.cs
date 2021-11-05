using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Utilities/Security/Jwt/JwtHelper.cs
    public class JwtTokenHelper : ITokenHelper {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtTokenHelper(IConfiguration configuration) {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims) {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiresInMinutes);
            var securityKey = SecurityKeyTool.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsTool.CreateSigningCredentials(securityKey);
            var jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, signingCredentials, user, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return new AccessToken {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(
            TokenOptions tokenOptions, SigningCredentials signingCredentials, User user, List<OperationClaim> operationClaims
        ) {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
        }

        private List<Claim> SetClaims(User user, List<OperationClaim> operationClaims) {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(oc => oc.Name).ToList());
            return claims;
        }
    }
}
