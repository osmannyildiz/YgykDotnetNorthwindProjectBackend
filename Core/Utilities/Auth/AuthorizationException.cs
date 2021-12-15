using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Auth {
    // https://docs.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
    public class AuthorizationException : Exception {
        public AuthorizationException() {}
        public AuthorizationException(string message) : base(message) {}
        public AuthorizationException(string message, Exception inner) : base(message, inner) {}
    }
}
