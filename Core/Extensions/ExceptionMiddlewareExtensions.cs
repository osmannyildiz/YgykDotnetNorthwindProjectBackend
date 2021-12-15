using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Extensions/ExceptionMiddlewareExtensions.cs
    public static class ExceptionMiddlewareExtensions {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app) {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
