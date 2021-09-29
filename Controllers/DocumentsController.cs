using AutoMapper;
using FloraYFaunaAPI.Commands.Document;
using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Exceptions;
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
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _webHostEnvironment;
        public WorkFiles _workFiles;
        private readonly IMapper _mapper;
        private readonly TokenUser tUser;
        private readonly string AppDirectory;

        public DocumentsController(ApplicationDbContext context, IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _workFiles = new WorkFiles();
            tUser = new TokenUser(accessor.HttpContext.User);
            AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/documents/");
        }

        // POST: api/Documents
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DocumentViewModel>> PostDocument([FromForm] CreateDocumentCommand command)
        {
            try
            {
                var extension = "." + command.File.FileName.Split('.')[command.File.FileName.Split('.').Length - 1];
                var fileName = DateTime.Now.Ticks + extension;
                _workFiles.createIfNoExistFolder(_webHostEnvironment.WebRootPath + "\\Uploads\\documents\\");
                var path = _webHostEnvironment.WebRootPath + "\\Uploads\\documents\\" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await command.File.CopyToAsync(stream);
                }
                string mimeType = _workFiles.GetMimeType(fileName);
                var document = new Document()
                {
                    Title = command.Title,
                    Description = command.Description,
                    FileName = fileName,
                    Extension = extension,
                    MimeType = mimeType
                };
                _context.Documents.Add(document);
                _context.SaveChanges(tUser);
                return await Task.FromResult(Ok( new { id = document.Id }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/Documents
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DocumentViewModel>>> GetDocuments()
        {
            var documents = await _context.Documents.ToListAsync();
            return _mapper.Map<List<DocumentViewModel>>(documents);
        }

        // GET: api/Documents
        [HttpGet("read")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DocumentViewModel>> GetDocument([FromBody] ReadDocumentCommand command)
        {
            var document = await _context.Documents.FindAsync(command.Id);
            if (document == null)
            {
                return NotFound();
            }

            return _mapper.Map<DocumentViewModel>(document);
        }

        // PUT: api/Documents
        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDocument([FromBody] UpdateDocumentCommand command)
        {
            var document = await _context.Documents.FindAsync(command.Id);
            if (document == null)
            {
                return NotFound();
            }
            else
            {
                document.Title = command.Title;
                document.Description = command.Description;
                _context.SaveChanges(tUser);
                return NoContent();
            }
        }

        // DELETE: api/Documents
        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDocument([FromBody] DeleteDocumentCommand command)
        {
            var document = await _context.Documents.FindAsync(command.Id);
            if (document == null)
            {
                return NotFound();
            }

            var path = _webHostEnvironment.WebRootPath + "\\Uploads\\documents\\" + document.FileName;
            if (System.IO.File.Exists(path))
            {

                System.IO.File.Delete(path);
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<FileResult> DownloadFile([FromBody] DownloadDocumentCommand command)
        {
            var document = await _context.Documents.FindAsync(command.Id);
            if(document == null)
            {
                throw new BadRequestException($"El documento solicitado no se encuentra disponoble para descargar");
            }
            
            string path = Path.Combine(AppDirectory, document.FileName);
            if (string.IsNullOrEmpty(path))
            {
                throw new BadRequestException($"El documento solicitado no se encuentra disponoble para descargar");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(path,FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, document.MimeType, document.MimeType);
        }
    }
}
