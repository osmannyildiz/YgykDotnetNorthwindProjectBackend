using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Auth;
using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Aspects.Autofac.Auth {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Business/BusinessAspects/Autofac/SecuredOperation.cs
    public class SecuredOperation : MethodInterception {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles) {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceHelper.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation) {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles) {
                if (roleClaims.Contains(role)) {
                    return;
                }
            }
            throw new AuthorizationException(Messages.AuthorizationDenied);

            // TODO Ensure HTTP status 401 for both unauthenticated and unauthorized. See:
            // - https://stackoverflow.com/questions/3547474/correct-http-status-code-when-resource-is-available-but-not-accessible-because-o
            // - https://datatracker.ietf.org/doc/html/rfc2616#section-10.4.2
        }
    }
}
