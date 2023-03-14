using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Books.Data;
using Books.Models;
using AutoMapper;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDBContext _context;
        private readonly IMapper _mapper;

        public BooksController(BookDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDM>>> Get()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDM>> Get(string id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var bookDM = await _context.Books.FindAsync(id);

            if (bookDM == null)
            {
                return NotFound();
            }

            return bookDM;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, BookVM bookVM)
        {
            if (id != bookVM.ID)
            {
                return BadRequest();
            }

            BookDM bookDM = _mapper.Map<BookDM>(bookVM);

            _context.Entry(bookDM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookDMExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookDM>> Post(BookVM bookVM)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookDBContext.Books'  is null.");
            }

            BookDM bookDM = _mapper.Map<BookDM>(bookVM);

            _context.Books.Add(bookDM);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookDMExists(bookDM.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Get", new { id = bookDM.ID }, bookDM);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var bookDM = await _context.Books.FindAsync(id);
            if (bookDM == null)
            {
                return NotFound();
            }

            _context.Books.Remove(bookDM);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookDMExists(string id)
        {
            return (_context.Books?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
