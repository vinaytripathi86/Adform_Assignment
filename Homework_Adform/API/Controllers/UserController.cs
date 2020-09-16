using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Adform.TodoAPI.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Create new instance of <see cref="ItemResolver"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="userService">User service.</param>
        /// <param name="appSettings">App settings.</param>
        public UserController(ILogger<UserController> logger, IUserService userService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="request">Login request.</param>
        /// <returns>Ok if successful.</returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginRequest request)
        {
            _logger.LogInformation("Authenticate");
            long? userId = await _userService.AuthenticateUser(request.UserName, request.Password);
            if (!userId.HasValue)
                return BadRequest(new { message = "Username or password is incorrect" });

            // authentication successful so generate jwt token
            var token = generateJwtToken(userId.Value, request.UserName);
            return Ok(new APIResponse<string> { IsSucess = true, Result = token });
        }

        private string generateJwtToken(long userId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}