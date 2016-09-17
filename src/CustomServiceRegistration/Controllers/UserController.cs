using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Models;
using CustomServiceRegistration.Services.Users;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Creates a user in registration service.
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
        /// <summary>
        /// Updating user data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var completed = await _userService.EditUserAsync(model);

                if (completed)
                {
                    return new NoContentResult();
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
