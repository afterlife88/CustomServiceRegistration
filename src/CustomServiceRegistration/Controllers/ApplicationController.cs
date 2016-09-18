using System;
using System.Threading.Tasks;
using System.Web.Http;
using CustomServiceRegistration.Models;
using CustomServiceRegistration.Services.Applications;
using Microsoft.AspNetCore.Mvc;


namespace CustomServiceRegistration.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        /// <summary>
        /// Add application to our service
        /// </summary>
        /// <remarks>
        /// 
        /// 
        ///    Registering application in service.
        ///    <br/>
        ///    After you will receive 201 you can make request to get token
        ///    <br/>
        /// 
        ///     For receive token you must make post request on <b>/api/token</b>
        ///     <br/>
        ///     With Content-Type: <b>'application/x-www-form-urlencoded'</b> 
        ///     <br/>
        ///     And with one parameter <b>appname</b> argument it's obviously your application name.
        ///     <br/>
        ///     <br/>
        ///     <b>Remember token for application and for user it's different tokens, so you will recive 401 if you will try to edit data of user with application token.
        ///     <br/>
        ///     Application token using only for access user data with email parameter.</b>
        ///     
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="201">Returns if application created successfully</response>
        /// <response code="400">Returns if application already exist or bad requested app name</response>
        /// <response code="500">Returns if server error has occurred</response>
        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodeResult), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationModel model)
        {
            try
            {
                var completed = await _applicationService.AddApplicationAsync(model.Name);

                if (completed)
                {
                    return StatusCode(201);
                }
                return BadRequest("Application with requested name already exist!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
