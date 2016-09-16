using System;
using System.Threading.Tasks;
using System.Web.Http;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Models;
using CustomServiceRegistration.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace CustomServiceRegistration.Controllers
{
    /// <summary>
    /// Main user controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            _userService = new UserService(ModelState, userRepository);
        }
        /// <summary>
        /// Creates a user in our registration service.
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Returns if user created successfully</response>
        /// <response code="400">Returns if some required fields are missing in request</response>
        /// <response code="500">Returns if server error has occurred</response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(OkResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var completed = await _userService.CreateAsync(model);

                if (completed)
                {
                    return StatusCode(201);
                }
                return BadRequest(_userService.ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
