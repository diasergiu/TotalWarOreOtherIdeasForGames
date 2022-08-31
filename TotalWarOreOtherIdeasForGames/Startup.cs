using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using TotalWarOreOtherIdeasForGames.Middleware;
using Microsoft.AspNetCore.Http;
using TotalWarDLA.Models.Enum;
using TotalWarOreOtherIdeasForGames.DataBaseOperations.Autorization;
using Microsoft.AspNetCore.Authorization;

namespace TotalWarOreOtherIdeasForGames
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddControllers();
            services.AddMvc();

            //// dont forget to delete this if it workes
            //services.AddMemoryCache();
            //services.AddSession();

            // if this workes i might not need memory cacher
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                // is this how you do it/ also i assume that i target httpPost methods
                options.LoginPath = new PathString("/Login");
                options.LogoutPath = new PathString("/Login/Logout");
            });
            services.AddSingleton<IAuthorizationHandler, AutorizationHandlerPermision>();
            services.AddAuthorization(options =>
            {              
                //options.AddPolicy("Manager", policy => policy.Requirements.Add(new RoleAuthorization("Manager")));
                //options.AddPolicy("Normal", policy => policy.Requirements.Add(new RoleAuthorization("Normal")));
                //options.AddPolicy("Admin", policy => policy.Requirements.Add(new RoleAuthorization("Admin")));

                // i dont know how i get the role from my users. i dont actually know what anything about roles
                // how do i make him understant witch varialbe should he compare to
                options.AddPolicy("RolebasedPolicy", policy => policy.RequireRole("Admin", "Manager", "Normal"));
            });
            services.AddDbContext<TotalWarWanaBeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddFile("Logs/Log-{Date}.txt");
            app.UseHttpsRedirection();
            //app.UseSession();
            //// dont forget to delete this 2 if the coockie workes
            //app.UseMiddleware<AuthentificationMiddleware>();
            //app.UseMiddleware<AuthorizationMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            // it saide something about this beeing in between UseRouting and UseEndpoint
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "Views")),
                RequestPath = "/Views"
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
