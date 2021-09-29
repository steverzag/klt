using AutoMapper;
using FloraYFaunaAPI.Commands.Carousel;
using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Helpers;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarouselController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public static IWebHostEnvironment _webHostEnvironment;
        public WorkFiles _workFiles;
        private readonly TokenUser tUser;

        public CarouselController(ApplicationDbContext context, IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _workFiles = new WorkFiles();
            tUser = new TokenUser(accessor.HttpContext.User);
        }

        // POST: api/Carousel
        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarouselViewModel>> PostCarousel([FromForm] CreateCarouselCommand command)
        {
            try
            {
                var extension = "." + command.File.FileName.Split('.')[command.File.FileName.Split('.').Length - 1];
                var fileName = DateTime.Now.Ticks + extension;
                _workFiles.createIfNoExistFolder(_webHostEnvironment.WebRootPath + "\\Uploads\\carousel\\");
                var path = _webHostEnvironment.WebRootPath + "\\Uploads\\carousel\\" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await command.File.CopyToAsync(stream);
                }

                var carousel = new Carousel()
                {
                    Title = command.Title,
                    Caption = command.Caption,
                    FileName = fileName,
                    Extension = extension,
                    Enabled = false,
                };
                _context.Carousel.Add(carousel);
                _context.SaveChanges(tUser);
                return await Task.FromResult(Ok(new { id = carousel.Id }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


        // GET: api/Carousel
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CarouselViewModel>>> GetCarousels()
        {
            var carousel = await _context.Carousel.ToListAsync();
            return _mapper.Map<List<CarouselViewModel>>(carousel);
        }

        // GET: api/Carousel
        [HttpGet("read")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarouselViewModel>> GetCarousel([FromBody] ReadCarouselCommand command)
        {
            var carousel = await _context.Carousel.FindAsync(command.Id);
            if (carousel == null)
            {
                return NotFound();
            }

            return _mapper.Map<CarouselViewModel>(carousel);
        }

        // PUT: api/Carousel
        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCarousel([FromBody] UpdateCarouselCommand command)
        {
            var carousel = await _context.Carousel.FindAsync(command.Id);
            if (carousel == null)
            {
                return NotFound();
            }
            else
            {
                carousel.Title = command.Title;
                carousel.Caption = command.Caption;
                _context.SaveChanges(tUser);
                return NoContent();
            }
        }

        // DELETE: api/Carousel
        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCarousel([FromBody] DeleteCarouselCommand command)
        {
            var carousel = await _context.Carousel.FindAsync(command.Id);
            if (carousel == null)
            {
                return NotFound();
            }

            var path = _webHostEnvironment.WebRootPath + "\\Uploads\\carousel\\" + carousel.FileName;
            if (System.IO.File.Exists(path))
            {

                System.IO.File.Delete(path);
                _context.Carousel.Remove(carousel);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Carousel
        [HttpPut("disabled-enabled")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDisabledEnabled([FromBody] DisabledEnabledCarouselCommand command)
        {
            var carousel = await _context.Carousel.FindAsync(command.Id);
            if (carousel == null)
            {
                return NotFound();
            }

            carousel.Enabled = !carousel.Enabled;
            _context.SaveChanges(tUser);
            return NoContent();
        }
    }
}
