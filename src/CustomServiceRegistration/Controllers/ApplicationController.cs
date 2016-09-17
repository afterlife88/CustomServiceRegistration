using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Models;
using CustomServiceRegistration.Models;
using Microsoft.AspNetCore.Mvc;


namespace CustomServiceRegistration.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        private readonly IApplicationRepository _applicationRepository;
        public ApplicationController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        /// <summary>
        /// Add application to our service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationModel model)
        {
            var app = new Application() { ApplicationName = model.Name };

            await _applicationRepository.Create(app);
            return StatusCode(201);

        }
    }
}
