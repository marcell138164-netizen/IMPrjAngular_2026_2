using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;

        public AutoresController(BibliotecaDbContext context)
        {
            _context = context;
        }

        // 1. LISTAR TODOS LOS AUTORES
        // GET: api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            var autores = await _context.Autores.ToListAsync();

            return Ok(autores);
        }

        // 2. BUSCAR UN AUTOR POR ID
        // GET: api/autores/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        // 3. INSERTAR UN AUTOR
        // POST: api/autores
        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // El ID se genera automaticamente en MySQL.
            autor.Id = 0;

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAutor),
                new { id = autor.Id },
                autor
            );
        }

        // 4. MODIFICAR UN AUTOR
        // PUT: api/autores/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest(
                    "El ID de la URL no coincide con el ID del autor."
                );
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var autorExistente = await _context.Autores.FindAsync(id);

            if (autorExistente == null)
            {
                return NotFound();
            }

            autorExistente.nombre = autor.nombre;
            autorExistente.descripcion = autor.descripcion;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 5. ELIMINAR UN AUTOR
        // DELETE: api/autores/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}