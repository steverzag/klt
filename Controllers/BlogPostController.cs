using AutoMapper;
using FloraYFaunaAPI.Commands.BlogPost;
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
using System.Linq;
using System.Threading.Tasks;


namespace FloraYFaunaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public static IWebHostEnvironment _webHostEnvironment;
        public WorkFiles _workFiles;
        private readonly TokenUser tUser;

        public BlogPostController(ApplicationDbContext context, IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _workFiles = new WorkFiles();
            tUser = new TokenUser(accessor.HttpContext.User);
        }

        // POST: api/BlogPost
        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlogPostViewModel>> PostPost([FromForm] CreateBlogPostCommand command)
        {
            var extension = "." + command.File.FileName.Split('.')[command.File.FileName.Split('.').Length - 1];
            var fileName = DateTime.Now.Ticks + extension;
            _workFiles.createIfNoExistFolder(_webHostEnvironment.WebRootPath + "\\Uploads\\blog\\");
            var path = _webHostEnvironment.WebRootPath + "\\Uploads\\blog\\" + fileName;
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await command.File.CopyToAsync(stream);
            }
            var post = new BlogPost()
            {
                Slug = _workFiles.GenerateSlug(command.Title),
                Title = command.Title,
                Description = command.Description,
                Author = command.Author,
                FileName = fileName,
                Extension = extension,
                Enabled = false,
                CategoryId = command.CategoryId
            };
            _context.BlogPosts.Add(post);
            _context.SaveChanges(tUser);
            return await Task.FromResult(Ok(new { id = post.Id }));
        }

        // GET: api/BlogPost
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BlogPostViewModel>>> GetPosts()
        {
            var posts = await _context.BlogPosts.ToListAsync();
            return _mapper.Map<List<BlogPostViewModel>>(posts);
        }

        // GET: api/BlogPost/5
        [HttpGet("read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlogPostViewModel>> GetPost([FromBody] ReadBlogPostCommand command)
        {
            var post = await _context.BlogPosts.FindAsync(command.Id);
            if (post == null)
            {
                return NotFound();
            }
            return _mapper.Map<BlogPostViewModel>(post);
        }

        // PUT: api/BlogPost
        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPost([FromBody] UpdateBlogPostCommand command)
        {
            var post = await _context.BlogPosts.FindAsync(command.Id);
            if (post == null)
            {
                return NotFound();
            }
            post.Title = command.Title;
            post.Slug = _workFiles.GenerateSlug(post.Title);
            post.Description = command.Description;
            post.Author = command.Author;
            post.CategoryId = command.CategoryId;
            await _context.SaveChangesAsync();
            _context.SaveChanges(tUser);
            return NoContent();
        }

        // DELETE: api/BlogPost
        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletPost([FromBody] DeleteBlogPostCommand command)
        {
            var post = await _context.BlogPosts.FindAsync(command.Id);
            if (post == null)
            {
                return NotFound();
            }

            var path = _webHostEnvironment.WebRootPath + "\\Uploads\\blog\\" + post.FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                _context.BlogPosts.Remove(post);
                _context.SaveChanges(tUser);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // PUT Like: api/BlogPost
        [HttpPut("likes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlogPostViewModel>> PutLike([FromBody] LikeBlogPostCommand command)
        {
            var post = await _context.BlogPosts.FindAsync(command.Id);
            if (post == null)
            {
                return NotFound();
            }
            string ipAddress = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            var ip = await _context.IpPosts.Where(x => x.IpAddress.Equals(ipAddress)).FirstOrDefaultAsync();
            if (ip == null)
            {
                post.Likes++;
                var ipPost = new IpPost()
                {
                    BlogPostId = command.Id,
                    IpAddress = ipAddress
                };
                _context.IpPosts.Add(ipPost);
                await _context.SaveChangesAsync();
                return await Task.FromResult(Ok(new { likes = post.Likes }));
            }
            return NoContent();           
        }

        // PUT Publish: api/BlogPost
        [HttpPut("publish")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlogPostViewModel>> PutPublish([FromBody] PublishBlogPostCommand command)
        {
            var post = await _context.BlogPosts.FindAsync(command.Id);
            if (post == null)
            {
                return NotFound();
            }
            post.PublishDate = DateTime.Now;
            post.Enabled = true;
            _context.SaveChanges(tUser);
            return NoContent();
        }
    }
}
