using AuthorsAPI.Contexts;
using AuthorsAPI.Entities;
using AuthorsAPI.Helpers.Filters;
using AuthorsAPI.Models.DTO;
using AuthorsAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _autoMapper;

        public AuthorsController(ApplicationDbContext dbContext, ILogger<AuthorsController> logger, IMapper autoMapper)
        {
            _dbContext = dbContext;
            _logger = logger;

            _logger.LogDebug("Authors controller was initialized");
            _autoMapper = autoMapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(CustomActionFilter))]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthorsAsync()
        {
            try
            {
                var authors = await _dbContext?.Authors?.Include(a => a.Books)?.ToListAsync();
                var authorsDTO = _autoMapper.Map<List<AuthorDTO>>(authors);
                return authorsDTO;
            }
            catch (Exception ex)
            {

                var m = ex.Message;
                return NotFound();
            }
            
        }

        [HttpGet("{id}", Name = nameof(GetAuthorByIdAsync))]
       // [Authorize]
        public async Task<ActionResult<AuthorDTO>> GetAuthorByIdAsync(int id)
        {
            var author = await _dbContext?.Authors?.Include(a => a.Books)?.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                _logger?.LogWarning("Invalid Author");
                return NotFound();
            }

            _logger?.LogInformation("Author found");

            return _autoMapper.Map<AuthorDTO>(author);
        }

        //api/authors/GetAuthor/{id}/{name} (optional)
        [HttpGet("GetAuthor/{id}/{name?}")]
        public async Task<ActionResult<Author>> GetAuthorByIdOrNameAsync(int id, string name)
        {
            var author = string.IsNullOrEmpty(name) ? 
                await _dbContext?.Authors?.Include(a => a.Books)?.FirstOrDefaultAsync(a => a.Id == id) :
                await _dbContext?.Authors?.Include(a => a.Books)?.FirstOrDefaultAsync(a => a.Id == id && a.Name == name);

            if (author == null)
                return NotFound();

            return author;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthorAsync([FromBody]AuthorCreateDTO authorCreate, CancellationToken ct = default)
        {
            //It is not necesary after NET Core 2.1, since the ApiController manages it.
            TryValidateModel(authorCreate);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (authorCreate == null)
                return BadRequest();

            var author = _autoMapper.Map<Author>(authorCreate);

            await _dbContext.Authors.AddAsync(author);

            await _dbContext.SaveChangesAsync();

            _logger?.LogInformation("New author was created");

            //Returns a 202 (Created), the route name and the response values.
            var authorDTO = _autoMapper.Map<AuthorDTO>(author);
            return new CreatedAtRouteResult(nameof(GetAuthorByIdAsync), new { id = author.Id }, authorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorAsync(int id, [FromBody]AuthorCreateDTO authorUpdate)
        {
            var author = _autoMapper.Map<Author>(authorUpdate);
            author.Id = id;
            //if (author == null || id != author.Id)
            //    return BadRequest();

            _dbContext.Entry(author).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            //return Ok(author.Id);
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchAuthorAsync(int id, [FromBody] JsonPatchDocument<AuthorCreateDTO> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var dbAuthor = await _dbContext.Authors.FirstOrDefaultAsync(a=>a.Id == id);

            if (dbAuthor == null)
                return NotFound();

            var authorDTO = _autoMapper?.Map<AuthorCreateDTO>(dbAuthor);            
            
            patchDocument.ApplyTo(authorDTO, ModelState);

            _autoMapper.Map(authorDTO, dbAuthor);

            var isValid = TryValidateModel(dbAuthor);
            if (!isValid)
                return BadRequest(ModelState);



            await _dbContext.SaveChangesAsync();

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAuthorAsync(int id)
        {
            var authorId = await _dbContext.Authors.Select(a=>a.Id).FirstOrDefaultAsync(i=>i == id);

            if (authorId == default(int))
            {
                _logger?.LogWarning("The author was not found");
                return NotFound();
            }

            _dbContext.Authors.Remove(new Author { Id = authorId});
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
