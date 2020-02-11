using AuthorsAPI.Contexts;
using AuthorsAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _dbContext.Books?.Include(b => b.Author)?.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _dbContext.Books?.Include(b=>b.Author)?.FirstOrDefaultAsync(b=>b.Id==id);

            if (book == null)
                return NotFound();

            return book;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody]Book book)            
        {
            if (book == null)
                return BadRequest();

            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return new CreatedAtRouteResult("GetBookById",new { id= book.Id,book});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id,[FromBody]Book book)
        {
            if (book == null || id != book.Id)
                return BadRequest();

            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return Ok(book.Id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return Ok(book.Id);
        }



    }
}
