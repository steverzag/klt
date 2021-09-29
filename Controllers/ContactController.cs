using AutoMapper;
using FloraYFaunaAPI.Commands.Contact;
using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Models;
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
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenUser tUser;

        public ContactController(ApplicationDbContext context, IHttpContextAccessor accessor, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            tUser = new TokenUser(accessor.HttpContext.User);
        }

        // POST: api/Contact/create
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContactViewModel>> PostContact([FromBody] CreateContactCommand command)
        {
            var contact = new Contact()
            {
                Name =  command.Name,
                Email = command.Email,
                Subject = command.Subject,
                Message = command.Message
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return await Task.FromResult(Ok(new { id = contact.Id }));
        }

        // GET: api/Contact/list
        [HttpGet("list")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ContactViewModel>>> GetContacts()
        {
            var contact = await _context.Contacts.ToListAsync();
            return _mapper.Map<List<ContactViewModel>>(contact);
        }

        // GET: api/Contact/read
        [HttpGet("read")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContactViewModel>> GetContact([FromBody] ReadContactCommand command)
        {
            var contact = await _context.Contacts.FindAsync(command.Id);
            if (contact == null)
            {
                return NotFound();
            }
            return _mapper.Map<ContactViewModel>(contact);
        }

        // PUT: api/Contact/update
        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkAsRead([FromBody] UpdateContactCommand command)
        {
            var contact = await _context.Contacts.FindAsync(command.Id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.MarkAsRead = true;
            _context.SaveChanges(tUser);
            return NoContent();
        }

        // PUT: api/Contact/update
        [HttpPut("update-all")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkAsReadAll([FromBody] UpdateContactAllCommand command)
        {
            foreach(Guid i in command.list)
            {
                var contact = await _context.Contacts.FindAsync(i);
                if (contact == null)
                {
                    return NotFound();
                }
                contact.MarkAsRead = true;
            }
            _context.SaveChanges(tUser);
            return NoContent();
        }

        // DELETE: api/Contact/delete
        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteContact([FromBody] DeleteContactCommand command)
        {
            var contact = await _context.Contacts.FindAsync(command.Id);
            if (contact == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
