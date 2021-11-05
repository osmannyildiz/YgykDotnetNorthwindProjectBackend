using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption {
    public class SecurityKeyTool {
        public static SecurityKey CreateSecurityKey(string securityKeyString) {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyString));
        }
    }
}
