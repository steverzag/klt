using FloraYFaunaAPI.Commands.Login;
using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Exceptions;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly AppSettingsModel _appSettings;
        private readonly IUserServices UserServices;

        public LoginController(ApplicationDbContext context, IOptions<AppSettingsModel> appSettingsAccessor, IUserServices userServices)
        {
            _context = context;
            _appSettings = appSettingsAccessor.Value;
            UserServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] LoginRequest login)
        {
            if (login == null)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }
            var user = _context.Users.SingleOrDefault(x => x.Username.Equals(login.Username));
            if (user == null)
            {
                return Unauthorized();
            }

            if(!UserServices.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            var token = TokenGenerator.GenerateTokenJwt(user, _appSettings);            
            AuthenticateResponse response = UserServices.Authenticated(user, token, ipAddress());
            setTokenCookie(response.RefreshToken);
            return await Task.FromResult(Ok(new { Token = token }));
        }

        [HttpPost("close-session")]
        public async Task<ActionResult> CloseSession([FromBody] CloseSessionCommand command)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            return await Task.FromResult(Ok(userId));
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            string refreshToken = Request.Cookies["refreshToken"];
            var response = UserServices.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromBody] RevokeTokenCommand command)
        {
            var token = command.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "El token es requerido" });
            UserServices.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = UserServices.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = UserServices.GetById(id);
            return Ok(user);
        }

        [HttpGet("{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(Guid id)
        {
            var user = UserServices.GetById(id);
            return Ok(user.RefreshToken);
        }
    }
}
