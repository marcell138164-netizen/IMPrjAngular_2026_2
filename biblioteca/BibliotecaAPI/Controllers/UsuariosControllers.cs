using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;

        public UsuariosController(BibliotecaDbContext context)
        {
            _context = context;
        }

        // 1. LISTAR TODOS LOS USUARIOS
        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return Ok(usuarios);
        }

        // 2. BUSCAR UN USUARIO POR ID
        // GET: api/usuarios/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // 3. INSERTAR UN USUARIO
        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // El ID se genera automaticamente en MySQL.
            usuario.id_usuario  = 0;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUsuario),
               new { id = usuario.id_usuario }
            );
        }

        // 4. MODIFICAR UN USUARIO
        // PUT: api/usuarios/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if  (id != usuario.id_usuario)
            {
                return BadRequest(
                    "El ID de la URL no coincide con el ID del usuario."
                );
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            usuarioExistente.rut = usuario.rut;
            usuarioExistente.dv = usuario.dv;
            usuarioExistente.nombre = usuario.nombre;
            usuarioExistente.apellido = usuario.apellido;
            usuarioExistente.email = usuario.email;
            usuarioExistente.celular = usuario.celular;
            usuarioExistente.fecha_ingreso = usuario.fecha_ingreso;
            usuarioExistente.direccion = usuario.direccion;
            

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 5. ELIMINAR UN USUARIO
        // DELETE: api/usuarios/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}