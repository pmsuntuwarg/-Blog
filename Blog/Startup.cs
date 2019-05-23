﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Data.DbContext;
using Blog.Entities.Models.Identity;
using Blog.Infrastructure.Interfaces.Admin;
using Blog.Infrastructure.Services.Admin;
using Blog.Infrastructure.Repositories.Admin;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blog.Common.Helpers;

namespace Blog
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Custom Extension.
            services.DependencyResolver();

            services.ConfigureApplicationCookie(options => 
                {
                    options.LoginPath = "/login";
                });

            services.AddHttpContextAccessor();

            services.AddMvc();
            
            services.AddMiniProfiler(options => {
                options.RouteBasePath = "/profiler";
            }).AddEntityFramework();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles(); 
            app.UseAuthentication();

            app.UseMiniProfiler();

            app.UseMvcWithCustomizedRoute();
        }
    }
}
