using CoreGuide.BLL.Business.Manager.Accounts;
using CoreGuide.BLL.Models.Accounts.RegisterUser.Input;
using CoreGuide.BLL.Models.Accounts.SignIn.Input;
using CoreGuide.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]

    public class AccountsController : ControllerBase
    {
        private readonly IAccountsManager _accountsManager;

        public AccountsController(IAccountsManager accountsManager)
        {
            _accountsManager = accountsManager;
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="input">RegisterUserInput</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterUserInput input)
        {
            var output = await _accountsManager.RegisterEmployee(input);
            return Created("/auth",output);
        }

        /// <summary>
        /// User Sign in
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User access and refresh tokens</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "UserName": "waleed.hegazy@orange.com",
        ///        "Password": "Orange@123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns new access and refresh tokens</response>
        /// <response code="400">If the user is not found</response>
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        //[CoreGuide.API.Utilities.Filters.ConsoleLogFilter]
        public async Task<IActionResult> SignIn([FromBody] SignInInput input, CancellationToken cancellationToken)
        {
            var output = await _accountsManager.SignIn(input, cancellationToken);
            return Ok(output);
        }

        [HttpGet("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromHeader] string refreshToken, CancellationToken cancellationToken)
        {
            var output = await _accountsManager.RefreshToken(refreshToken, cancellationToken);
            return Ok(output);
        }

        [HttpGet("Auth")]
        public IActionResult Auth()
        {
            var name = HttpContext.User.Identity.Name;
            return Ok($"{name} is authenticated");
        }

        [HttpGet("Role")]
        [Authorize(Roles = Strings.Policies.Admin)]
        public IActionResult Role()
        {
            var name = HttpContext.User.Identity.Name;
            return Ok($"{name} is authenticated and authorized");
        }

        [HttpGet("Policy")]
        [Authorize(Policy = Strings.Policies.User)]
        public IActionResult Policy()
        {
            var name = HttpContext.User.Identity.Name;
            return Ok($"{name} is authenticated and authorized");
        }


    }
}
