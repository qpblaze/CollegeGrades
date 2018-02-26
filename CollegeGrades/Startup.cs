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

namespace CollegeGrades
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                                    .SetBasePath(env.ContentRootPath)
                                    .AddJsonFile("secrets.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile($"secrets.{env.EnvironmentName}.json", optional: true)
                                    .AddUserSecrets<Startup>()
                                    .AddEnvironmentVariables()
                                    .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CollegeGrades")));

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/login";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
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
            app.UseMvcWithDefaultRoute();
        }
    }
}