using AuthorsAPI.Contexts;
using AuthorsAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            return _dbContext?.Authors?.ToList();
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _dbContext?.Authors?.FirstOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            return author;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromBody]Author author)
        {
            //It is not necesary after NET Core 2.1, since the ApiController manages it.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (author == null)
                return BadRequest();

            await _dbContext.Authors.AddAsync(author);

            await _dbContext.SaveChangesAsync();

            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody]Author author)
        {
            if (author == null || id != author.Id)
                return BadRequest();

            _dbContext.Entry(author).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return Ok(author.Id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAuthor(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);

            if (author == null)
                return NotFound();

            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();

            return Ok(author.Id);
        }
    }
}
