using AutoMapper;
using FloraYFaunaAPI.Commands.Newsletter;
using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NewsletterController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: api/Newsletter
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NewsletterViewModel>> PostNewsletter([FromBody] CreateNewsletterCommand command)
        {
            var newsletter = new Newsletter()
            {
                FullName = command.FullName,
                Email = command.Email,
            };
            _context.Newsletters.Add(newsletter);
            await _context.SaveChangesAsync();
            return await Task.FromResult(Ok(new { id = newsletter.Id }));
        }

        // GET: api/Newsletter
        [HttpGet("list")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<NewsletterViewModel>>> GetNewsletter()
        {
            var newsletters = await _context.Newsletters.ToListAsync();
            return _mapper.Map<List<NewsletterViewModel>>(newsletters);
        }

        // GET: api/Newsletter/5
        [HttpGet("read")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<NewsletterViewModel>> GetNewsletter([FromBody] ReadNewsletterCommand command)
        {
            var newsletters = await _context.Newsletters.FindAsync(command.Id);
            if (newsletters == null)
            {
                return NotFound();
            }
            return _mapper.Map<NewsletterViewModel>(newsletters);
        }

        // DELETE: api/Newsletter
        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteNewsletter([FromBody] DeleteNewsletterCommand command)
        {
            var newsletters = await _context.Newsletters.FindAsync(command.Id);
            if (newsletters == null)
            {
                return NotFound();
            }
            _context.Newsletters.Remove(newsletters);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
