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
            services.AddMemoryCache();
            services.AddSession();
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
            app.UseSession();
            app.UseMiddleware<AuthentificationMiddleware>();
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseRouting();

            
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
