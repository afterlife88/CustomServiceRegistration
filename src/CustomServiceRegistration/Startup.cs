using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;

namespace CustomServiceRegistration
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
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

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			
			app.UseSwagger();
		
			app.UseSwaggerUi();

			/*Normal MVC mappings*/
			app.UseMvc();
		}
	}
}
