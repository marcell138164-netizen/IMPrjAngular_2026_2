using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;

        public LibrosController(BibliotecaDbContext context)
        {
            _context = context;
        }

        // GET: api/libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> ObtenerLibros()
        {
            var libros = await _context.Libros
                .AsNoTracking()
                .OrderBy(l => l.id)
                .ToListAsync();

            return Ok(libros);
        }

        // GET: api/libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> ObtenerLibro(int id)
        {
            var libro = await _context.Libros
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.id == id);

            if (libro == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el libro."
                });
            }

            return Ok(libro);
        }

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<Libro>> CrearLibro(Libro libro)
        {
            _context.Libros.Add(libro);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(ObtenerLibro),
                new { id = libro.id },
                libro
            );
        }

        // PUT: api/libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarLibro(
            int id, Libro libro)
        {
            if (id != libro.id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID de la URL no coincide con el ID del libro."
                });
            }

            var existe = await _context.Libros
                .AnyAsync(l => l.id == id);

            if (!existe)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el libro."
                });
            }

            _context.Entry(libro).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarLibro(int id)
        {
            var libro = await _context.Libros
                .FirstOrDefaultAsync(l => l.id == id);

            if (libro == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el libro."
                });
            }

            _context.Libros.Remove(libro);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}