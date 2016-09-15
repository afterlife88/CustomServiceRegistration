using System;
using CustomServiceRegistration.Domain.Context;
using CustomServiceRegistration.Domain.Infrastructure.Configuration;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Infrastructure.Repositories;
using CustomServiceRegistration.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;

namespace CustomServiceRegistration
{
	public class Startup
	{
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
				)
				.AddEntityFrameworkStores<DataDbContext>();

			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			}).AddWebApiConventions();

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
			});

			// for seeding the database with the demo user details
			services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
			services.AddScoped<IUserRepository, UserRepository>();
		}


		public void Configure(IApplicationBuilder app, IDatabaseInitializer databaseInitializer)
		{
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUi();
			app.UseWelcomePage();

			databaseInitializer.Seed().GetAwaiter().GetResult();
		}
	}
}
