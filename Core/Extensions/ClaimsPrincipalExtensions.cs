using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Extensions/ClaimsPrincipalExtensions.cs
    public static class ClaimsPrincipalExtensions {
        public static List<string> GetClaims(this ClaimsPrincipal claimsPrincipal, string claimType) {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> GetRoleClaims(this ClaimsPrincipal claimsPrincipal) {
            return claimsPrincipal?.GetClaims(ClaimTypes.Role);
        }
    }
}
