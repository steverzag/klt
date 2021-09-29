using AutoMapper;
using FloraYFaunaAPI.Commands.Category;
using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Exceptions;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenUser tUser;

        public CategoriesController(ApplicationDbContext context, IHttpContextAccessor accessor,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            tUser = new TokenUser(accessor.HttpContext.User);
        }

        // POST: api/Category
        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryViewModel>> PostCategory([FromBody] CreateCategoryCommand command)
        {
            var cate = _context.Categories.Where(x => x.Name.Equals(command.Name)).FirstOrDefault();
            if(cate != null)
            {
                throw new BadRequestException($"Ya existe una categoria con el nombre {command.Name}");
            }
            else
            {
                var category = new Category()
                {
                    Name = command.Name,
                };
                _context.Categories.Add(category);
                _context.SaveChanges(tUser);
                return await Task.FromResult(Ok(new { id = category.Id }));
            }
        }

        // GET: api/Category
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }

        // GET: api/Category/5
        [HttpGet("read")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryViewModel>> GetCategory([FromBody] ReadCategoryCommand command)
        {
            var categories = await _context.Categories.FindAsync(command.Id);
            if (categories == null)
            {
                return NotFound();
            }
            return _mapper.Map<CategoryViewModel>(categories);
        }

        // PUT: api/Category
        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategory([FromBody] UpdateCategoryCommand command)
        {
            var category = await _context.Categories.FindAsync(command.Id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = command.Name;
            _context.SaveChanges(tUser);
            return NoContent();
        }

        // DELETE: api/Category
        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command)
        {
            var category = await _context.Categories.FindAsync(command.Id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
