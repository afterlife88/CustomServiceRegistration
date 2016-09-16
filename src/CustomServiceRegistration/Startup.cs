﻿using System;
using System.Collections.Generic;
using System.IO;
using CustomServiceRegistration.Domain.Context;
using CustomServiceRegistration.Domain.Infrastructure.Configuration;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Infrastructure.Repositories;
using CustomServiceRegistration.Domain.Models;
using CustomServiceRegistration.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;

namespace CustomServiceRegistration
{
    public partial class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;

        public Startup(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection =
                @"Server=tcp:itcompetitions.database.windows.net,1433;Database=devchallenge;User ID=admin@yo.com@itcompetitions;Password=nLcJZ8i9;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            services.AddDbContext<DataDbContext>(options => options.UseSqlServer(connection));

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

                //options.OperationFilter<AssignSecurityRequirements>();

            });
            if (_hostingEnv.IsDevelopment())
            {
                services.ConfigureSwaggerGen(c =>
                {
                    c.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));
                    
                });
            }
            // for seeding the database with the demo user details
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }


        public void Configure(IApplicationBuilder app, IDatabaseInitializer databaseInitializer)
        {
            ConfigureAuth(app);
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();
            app.UseCors(builder =>
                    // This will allow any request from any server. Tweak to fit your needs!
                    // The fluent API is pretty pleasant to work with.
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


            databaseInitializer.Seed().GetAwaiter().GetResult();
        }

        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "CustomServiceRegistration.xml");
        }
    }
}
