using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Models;
using CustomServiceRegistration.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        /// Return user data
        /// </summary>
        /// <param name="userEmail"></param>
        /// <response code="200">Return user data</response>
        /// <response code="400">Returns if passed value invalid</response>
        /// <response code="401">Returns if authorize token are missing in header or token is wrong</response>
        /// <response code="404">Returns if passed user are not exist</response>
        /// <response code="500">Returns if server error has occurred</response>
        [HttpGet("{userEmail}")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> GetUser(string userEmail)
        {
            try
            {

                var checkIfRequestFromApp = User.Claims.FirstOrDefault(r => r.Value == "app");
                if (checkIfRequestFromApp == null)
                    return Unauthorized();

                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest();
                }
                var validEmail = WebUtility.HtmlEncode(userEmail);

                var user = await _userService.GetUser(validEmail);

                if (user != null)
                {
                    return new ObjectResult(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
        /// <response code="204">Return if updated successfully</response>
        /// <response code="400">Returns if passed value invalid</response>
        /// <response code="401">Returns if authorize token are missing in header or token is wrong</response>
        /// <response code="500">Returns if server error has occurred</response>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            try
            {
                var checkIfRequestFromUserToken = User.Claims.FirstOrDefault(r => r.Value == "user");
                if (checkIfRequestFromUserToken == null)
                    return Unauthorized();

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
