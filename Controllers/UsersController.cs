using AutoMapper;
using FloraYFaunaAPI.Commands.User;
using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Enums;
using FloraYFaunaAPI.Exceptions;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.Services.Contract;
using FloraYFaunaAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserServices UserServices;
        private readonly TokenUser tUser;

        public UsersController(ApplicationDbContext context, IHttpContextAccessor accessor, IMapper mapper, IUserServices userServices)
        {
            _context = context;
            _mapper = mapper;
            UserServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            tUser = new TokenUser(accessor.HttpContext.User);
        }

        // POST: api/Users/create
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserViewModel>> PostUser([FromBody] CreateUserCommand command)
        {
            byte[] passwordHash, passwordSalt;
            UserServices.CreatePasswordHash(command.Password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                FullName = command.FullName,
                Username = command.UserName,
                Email = command.Email,
                Rol = command.Rol,
                Enabled = false
            };
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            _context.SaveChanges(tUser);
            return await Task.FromResult(Ok( new { id = user.UserId }));
        }

        // GET: api/Users/list
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        // GET: api/Users/read
        [HttpGet("read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserViewModel>> GetUser([FromBody] ReadUserCommand command)
        {
            var user = await _context.Users.FindAsync(command.Id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserViewModel>(user);
        }

        // PUT: api/Users/update
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUser([FromBody]  UpdateUserCommand command)
        {
            var user = await _context.Users.FindAsync(command.id);
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = command.FullName;
            user.Username = command.UserName;
            user.Email = command.Email;
            user.Rol = command.Rol;
            _context.SaveChanges(tUser);
            return NoContent();
        }

        // DELETE: api/Users/delete
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            var user = await _context.Users.FindAsync(command.Id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Users/disabled-enabled
        [HttpPut("disabled-enabled")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDisabledEnabled(DisabledEnabledUserCommand command)
        {
            var user = await _context.Users.FindAsync(command.Id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Rol.Equals(UserRol.SuperAdmin))
            {
                throw new BadRequestException($"Usted no tiene acceso para realizar este servicio");
            }

            user.Enabled = !user.Enabled;
            _context.SaveChanges(tUser);
            return NoContent();
        }

        // PUT: api/Users/set-password
        [HttpPut("set-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetPassword(SetPasswordUserCommand command)
        {
            var user = await _context.Users.FindAsync(command.Id);
            if (user == null)
            {
                throw new BadRequestException($"No se encuentra el user para el identificador {command.Id}");
            }
            else if (UserServices.VerifyPasswordHash(command.Password, user.PasswordHash,user.PasswordSalt))
            {
                throw new BadRequestException($"La contraseña no es correcta");
            }
            else
            {
                byte[] passwordHash, passwordSalt;
                UserServices.CreatePasswordHash(command.NewPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.SaveChanges(tUser);
                return NoContent();
            }
        }
    }
}
