using CollegeGrades.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using CollegeGrades.Infrastructure.Data;
using CollegeGrades.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Interfaces;
using CollegeGrades.Infrastructure.Services;

namespace CollegeGrades
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CollegeGradesConnection")));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
            });

            //Add the secrets
            services.AddOptions();
            services.Configure<AppSecrets>(Configuration);
            services.Configure<AppSecrets>(secrets =>
            {
                // SendGrid secrets
                secrets.SendGrid.ApiKey = Configuration["SendGrid:api_key"];
            });

            services.AddAutoMapper();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new RequireHttpsAttribute());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // For better security
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Forces the browser to reconnect to HTTPS
            app.UseHsts(options => options.MaxAge(days: 365).IncludeSubdomains());

            // Turns on cross site scripting prevention measures
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            // Prevents attacks with different content type
            app.UseXContentTypeOptions();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}