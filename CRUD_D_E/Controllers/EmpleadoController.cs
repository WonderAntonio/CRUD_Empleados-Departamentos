using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using CRUD_D_E.Data;

namespace CRUD_D_E.Controllers
{
	[Route("api/[controller]")]/// <summary> Define la ruta del controlador de API, en este caso "api/Empleado".</summary>
	[ApiController]/// <summary> Define que es un controlador de API.</summary>
	public class EmpleadoController : ControllerBase /// <summary> Define el controlador de API para la entidad Empleado, que permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) sobre los empleados.</summary>
	{
        private readonly Bd _context;/// <summary> Define el contexto de la base de datos, que se utiliza para acceder a las entidades y realizar operaciones sobre ellas.</summary>
		public EmpleadoController(Bd context)/// <summary> Constructor del controlador de API, que recibe el contexto de la base de datos mediante inyección de dependencias.</summary>
		{
            _context = context;
		}
        /// <summary>
        /// Consigue todos los empleados de la base de datos. GET: api/Empleado
        /// </summary>
        /// <returns>Devuelve una lista de todos los empleados.</returns>
        [HttpGet]
		public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
		{
			return await _context.Empleado.ToListAsync();
		}
		/// <summary>
        /// Obtiene un empleado por su identificador.
        /// </summary>
        /// <param name="id">Identificador del empleado.</param>
        /// <returns>El empleado encontrado; NotFound si no existe.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Empleado>> GetEmpleado(int id)
		{
			var empleado = await _context.Empleado.FindAsync(id);
			if (empleado == null)
			{
				return NotFound();
			}
			return empleado;
		}
        /// <summary>
        /// Obtiene todos los empleados inactivos.
        /// </summary>
        /// <returns>Una lista de empleados inactivos.</returns>
        [HttpGet("Inactivo")]
		public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosInactivos()
        {
            var empleadosInactivos = await _context.Empleado.Where(e => e.Estatus == EstatusEmpleado.Inactivo).ToListAsync();
            return Ok(empleadosInactivos);
        }
        /// <summary>
        /// Crea un nuevo empleado en la base de datos.
        /// </summary>
        /// <param name="empleado">Datos del empleado que se va a crear.</param>
        /// <returns>La respuesta de creación con el empleado agregado.</returns>
        [HttpPost]
		public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
		{
			_context.Empleado.Add(empleado);/// <summary> Agregar el empleado a la base de datos.</summary>
            await _context.SaveChangesAsync();/// <summary> Guardar los cambios en la base de datos de manera asincrona.</summary>

            return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, empleado);/// <summary> Devolver una respuesta 201 Created que contiene el empleado creado y la ruta para acceder a él.</summary>
        }
        /// <summary>
        /// Actualiza un empleado existente.
        /// </summary>
        /// <param name="id">Identificador del empleado que se va a actualizar.</param>
        /// <param name="empleado">Datos del empleado que se va a actualizar.</param>
        /// <returns>Una respuesta sin contenido si la actualización se realiza correctamente; BadRequest si los identificadores
        /// no coinciden o NotFound si el empleado no existe.</returns>
        [HttpPut("{id}")]
		public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
		{
			if (id != empleado.Id)/// <summary> Verificar que el identificador del empleado en la ruta coincide con el identificador del empleado en el cuerpo de la solicitud.</summary>
			{
                return BadRequest();/// <summary> Devolver una respuesta BadRequest si los identificadores no coinciden.</summary>  
			}
            _context.Entry(empleado).State = EntityState.Modified;/// <summary> Marcar el empleado como modificado en el contexto de la base de datos.</summary>
            try
            {
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)/// <summary> Capturar la excepción de concurrencia que se produce cuando se intenta actualizar un empleado que ya no existe en la base de datos.</summary>
            {
                if (!EmpleadoExists(id))
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
        /// <summary>
        /// Elimina un empleado existente de la base de datos. DELETE: api/Empleado/5
        /// </summary>
        /// <param name="id">El identificador del empleado a eliminar.</param>
        /// <returns>Un not-foun cuando el empleado no existe; de lo contrario, un resultado sin contenido.</returns>
        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            _context.Empleado.Remove(empleado);/// <summary> Eliminar el empleado de la base de datos.</summary>
            await _context.SaveChangesAsync();/// <summary> Guardar los cambios en la base de datos de manera asincrona.</summary>
            return NoContent();
        }
        private bool EmpleadoExists(int id)/// <summary> Verifica si un empleado existe en la base de datos por su identificador.</summary>
        {
            return _context.Empleado.Any(e => e.Id == id);/// <summary> Devolver true si el empleado existe, false si no.</summary>
        }
    }
}