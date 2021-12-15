using Core.Utilities.Auth;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Extensions/ExceptionMiddleware.cs
    public class ExceptionMiddleware {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            try {
                await _next(httpContext);
            } catch (Exception e) {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e) {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";
            httpContext.Response.ContentType = "application/json";

            if (e.GetType() == typeof(ValidationException)) {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = e.Message;

                httpContext.Response.StatusCode = statusCode;
                return httpContext.Response.WriteAsync(new ValidationErrorDetails {
                    StatusCode = statusCode,
                    Message = message,
                    ValidationFailures = ((ValidationException)e).Errors
                }.ToString());
            } else if (e.GetType() == typeof(AuthorizationException)) {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = e.Message;
            }
            
            httpContext.Response.StatusCode = statusCode;
            return httpContext.Response.WriteAsync(new ErrorDetails {
                StatusCode = statusCode,
                Message = message
            }.ToString());
        }
    }
}
