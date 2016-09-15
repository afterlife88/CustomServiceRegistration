using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			}).AddWebApiConventions();

			/*Adding swagger generation with default settings*/
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
			
			app.UseMvc(routes =>
			{
				routes.MapWebApiRoute(
					name: "apiRoute",
					template: "api/{controller}/{action}");

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}");
			});
			app.UseSwagger();
			app.UseSwaggerUi();
		

		}
	}
}
