﻿using System;
using CustomServiceRegistration.Domain.Context;
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

			services.AddIdentity<ApplicationUser, IdentityRole>()
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
		}


		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUi();
			app.UseWelcomePage();
		}

		private static async void CreateSampleData(IServiceProvider applicationServices)
		{
			using (var dbContext = applicationServices.GetService<DataDbContext>())
			{
				var sqlServerDatabase = dbContext.Database;
				if (sqlServerDatabase != null)
				{
					var manager = applicationServices.GetService<UserManager<ApplicationUser>>();
					await manager.FindByEmailAsync("test01@example.com");
					// add some users
					//var userManager = applicationServices.GetService<UserManager<ApplicationUser>>();
					//ApplicationUser user = await userManager.FindByEmailAsync("test01@example.com");
					//if (user == null)
					//{
					//	user = new ApplicationUser { UserName = "test01", Email = "test01@example.com" };
					//	await userManager.CreateAsync(user, "Qwer!@#12345");
					//}
				}
			}
		}
	}
}
