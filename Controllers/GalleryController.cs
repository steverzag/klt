using AutoMapper;
using FloraYFaunaAPI.Commands.Command.Gallery;
using FloraYFaunaAPI.Commands.Gallery;
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
    public class GalleryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _webHostEnvironment;
        public WorkFiles _workFiles;
        private readonly IMapper _mapper;
        private readonly TokenUser tUser;

        public GalleryController(ApplicationDbContext context, IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _workFiles = new WorkFiles();
            tUser = new TokenUser(accessor.HttpContext.User);
        }

        // POST: api/Gallery
        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GalleryViewModel>> PostGallery([FromForm] CreateGalleryCommand command)
        {
            try
            {
                var extension = "." + command.File.FileName.Split('.')[command.File.FileName.Split('.').Length - 1];
                var fileName = DateTime.Now.Ticks + extension;
                _workFiles.createIfNoExistFolder(_webHostEnvironment.WebRootPath + "\\Uploads\\gallery\\");
                var path = _webHostEnvironment.WebRootPath + "\\Uploads\\gallery\\" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await command.File.CopyToAsync(stream);
                }
                var image = new Gallery()
                {
                    Title = command.Title,
                    Description = command.Description,
                    Author = command.Author,
                    FileName = fileName,
                    Extension = extension,
                };
                _context.Gallery.Add(image);
                _context.SaveChanges(tUser);
                return await Task.FromResult(Ok(new { id = image.Id }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/Gallery
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GalleryViewModel>>> GetGalleries()
        {
            var gallery = await _context.Gallery.ToListAsync();
            return _mapper.Map<List<GalleryViewModel>>(gallery);
        }

        // GET: api/Gallery
        [HttpGet("read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GalleryViewModel>> GetGallery([FromBody] ReadGalleryCommand command)
        {
            var gallery = await _context.Gallery.FindAsync(command.Id);
            if (gallery == null)
            {
                return NotFound();
            }
            return _mapper.Map<GalleryViewModel>(gallery); ;
        }

        // PUT: api/Gallery
        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDocument([FromBody] UpdateGalleryCommand command)
        {
            var gallery = await _context.Gallery.FindAsync(command.Id);
            if (gallery == null)
            {
                return NotFound();
            }
            else
            {
                gallery.Title = command.Title;
                gallery.Description = command.Description;
                gallery.Author = command.Author;
                _context.SaveChanges(tUser);
                return NoContent();
            }
        }

        // DELETE: api/Gallery
        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGallery([FromBody] DeleteGalleryCommand command)
        {
            var gallery = await _context.Gallery.FindAsync(command.Id);
            if (gallery == null)
            {
                return NotFound();
            }

            var path = _webHostEnvironment.WebRootPath + "\\Uploads\\gallery\\" + gallery.FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                _context.Gallery.Remove(gallery);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
