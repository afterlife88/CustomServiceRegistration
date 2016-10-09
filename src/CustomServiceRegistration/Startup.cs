using System;
using System.IO;
using CustomServiceRegistration.Configurations;
using CustomServiceRegistration.Domain.Context;
using CustomServiceRegistration.Domain.Infrastructure.Configuration;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Infrastructure.Repositories;
using CustomServiceRegistration.Domain.Models;
using CustomServiceRegistration.Services.Applications;
using CustomServiceRegistration.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;

namespace CustomServiceRegistration
{
    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(options => options.UseInMemoryDatabase());

            services.AddIdentity<ApplicationUser, IdentityRole>(
                    pass =>
                    {
                        pass.Password.RequireDigit = false;
                        pass.Password.RequiredLength = 6;
                        pass.Password.RequireNonAlphanumeric = false;
                        pass.Password.RequireUppercase = false;
                        pass.Password.RequireLowercase = false;
                    }
                ).AddEntityFrameworkStores<DataDbContext>();

            services.AddMvc()
              .AddJsonOptions(options =>
              {
                  options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              });

            //Adding swagger generation with default settings
            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Custom service registration",
                    Description = "API documentation",
                    TermsOfService = "None"
                });
                options.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));


            });
            //services.ReplaceDefaultViewEngine();


            // Set angular locations
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new AngularAppViewLocationExpander());
            });
            // for seeding the database with the demo user details
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IApplicationService, ApplicationService>();
        }


        public void Configure(IApplicationBuilder app, IDatabaseInitializer databaseInitializer, IServiceProvider services)
        {
            ConfigureAuth(app, services);
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();
            app.UseCors(builder =>
                    // This will allow any request from any server. 
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

            // Add MVC to the request pipeline.
            app.UseDeveloperExceptionPage();
            app.UseMvc();

            app.UseSwagger((httpRequest, swaggerDoc) =>
            {
                swaggerDoc.Host = httpRequest.Host.Value;
            });

            app.UseSwaggerUi();
            app.UseMvcWithDefaultRoute();

            // Recreate db's
            databaseInitializer.Seed().GetAwaiter().GetResult();
        }

        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "CustomServiceRegistration.xml");
        }
    }
}
