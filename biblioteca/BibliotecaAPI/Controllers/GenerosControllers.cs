using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;

        public GenerosController(BibliotecaDbContext context)
        {
            _context = context;
        }

        // 1. LISTAR TODOS LOS GENEROS
        // GET: api/generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            var generos = await _context.Generos.ToListAsync();

            return Ok(generos);
        }

        // 2. BUSCAR UN GENERO POR ID
        // GET: api/generos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        // 3. INSERTAR UN GENERO
        // POST: api/generos
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // El ID se genera automaticamente en MySQL.
            genero.Id = 0;

            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGenero),
                new { id = genero.Id },
                genero
            );
        }

        // 4. MODIFICAR UN GENERO
        // PUT: api/generos/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            if (id != genero.Id)
            {
                return BadRequest(
                    "El ID de la URL no coincide con el ID del genero."
                );
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var generoExistente = await _context.Generos.FindAsync(id);

            if (generoExistente == null)
            {
                return NotFound();
            }

            generoExistente.nombre = genero.nombre;
            generoExistente.descripcion = genero.descripcion;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 5. ELIMINAR UN GENERO
        // DELETE: api/generos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}