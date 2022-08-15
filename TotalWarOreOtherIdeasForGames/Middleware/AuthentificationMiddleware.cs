using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models.Enum;

namespace TotalWarOreOtherIdeasForGames.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthentificationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthentificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            if (!path.Value.StartsWith("/Login") && httpContext.Session.GetString("UserName") == null)
            {
                httpContext.Response.Redirect("/Login");
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthentificationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthentificationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthentificationMiddleware>();
        }
    }
}
