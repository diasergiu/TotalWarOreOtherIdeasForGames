using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalWarOreOtherIdeasForGames.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    // used when i used session
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        // i dont have a message worning about unatorizations
        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            var session = httpContext.Session;
            // didnt work
            // it looks like when he comes back the authenrification middleware wrirights the path again and dosent hit the index with parameters 
            string message = "user level " + session.GetInt32("UserType") + "is unauthorized from " + path.Value + " page";
            if ((path.Value.StartsWith("/Barding") ||
                path.Value.StartsWith("/Horses") ||
                path.Value.StartsWith("/Items") ||
                path.Value.StartsWith("/Traits")) && session.GetInt32("UserType") < 1)
            {
                httpContext.Response.Redirect("/Home/message=" + message);
            }
            if ((path.Value.StartsWith("/Faction") ||
                path.Value.StartsWith("/Formation") ||
                path.Value.StartsWith("/SoldierModel")) && session.GetInt32("UserType") < 2)
            {
                httpContext.Response.Redirect("/Home/message=" + message);
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
