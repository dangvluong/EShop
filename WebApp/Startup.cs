using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Services;

namespace WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940\
        //IConfiguration configuration;
        //public Startup(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/auth/login";
            });
            services.AddScoped<IRepositoryManager,RepositoryManager>();
            services.AddScoped<IMailService, MailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            
            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("Default", pattern: "/{controller}/{action}/{id?}",defaults: new {controller="product",action="index"});
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(name: "dashboard", pattern: "{area:exists}/{controller=statistic}/{action=index}/{id?}");
            });
        }
    }
}
