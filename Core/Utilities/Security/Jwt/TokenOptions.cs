using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Utilities/Security/Jwt/TokenOptions.cs
    public class TokenOptions {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiresInMinutes { get; set; }
        public string SecurityKey { get; set; }
    }
}
