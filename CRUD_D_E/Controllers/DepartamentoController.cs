using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using CRUD_D_E.Data;

namespace CRUD_D_E.Controllers
{
    [Route("api/[controller]")]/// <summary> Define la ruta del controlador de API, en este caso "api/Departamento".</summary>
    [ApiController]/// <summary> Define que es un controlador de API.</summary>

   /// <summary> 
   /// Define el controlador de API para la entidad Departamento, que permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) sobre los departamentos.
   /// </summary>
   /// <remarks> Task para operaciones asincronas, ActionResult para devolver resultados de acciones HTTP, IEnumerable para devolver listas de entidades, EntityState para marcar el estado de las entidades en el contexto de la base de datos, 
   /// DbUpdateConcurrencyException para capturar excepciones de concurrencia al actualizar entidades, await para esperar la finalización de operaciones asincrónicas, y Any para verificar si existen entidades en la base de datos.
   /// </remarks>
    public class DepartamentoController : ControllerBase 
    {
        /// <summary> Define el contexto de la base de datos, que se utiliza para acceder a las entidades y realizar operaciones sobre ellas.</summary>
        private readonly Bd _context;
        /// <summary> 
        /// Constructor del controlador de API, que recibe el contexto de la base de datos mediante inyección de dependencias.
        /// </summary>
        /// <param name="context">El contexto de la base de datos.</param>                            
        public DepartamentoController(Bd context)
        {
            _context = context;
        }
        /// <summary>
        /// Devuleve todos los departamentos de la base de datos. GET: api/Departamento
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The result contains all departments.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()// crear un metodo asincrono que devuelva una lista de departamentos
        {
            return await _context.Departamento.ToListAsync();// devolver la lista de departamentos
        }
        /// <summary>
        /// Consige un departamento por su identificador. GET: api/Departamento/5
        /// </summary>
        /// <param name="id">El identificador del departamento. </param>
        /// <returns>El departamento que coincide con el identificador especificado, o un resultado de no encontrado cuando no existe ningún departamento.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return departamento;
        }
        /// <summary>
        /// Consigue los empleados de un departamento por su identificador. GET: api/Departamento/5/Empleados
        /// </summary>
        /// <param name="id">El identificador del departamento.</param>
        /// <returns>An <see cref="ActionResult"/> Contiene una lista de empleados que pertenecen al departamento especificado, o un resultado de no encontrado cuando el departamento no
        /// existe</returns>
        [HttpGet("{id}/Empleados")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetEmpleados(int id)
        {
            var departamento = await _context.Departamento.Include(d => d.Empleado).FirstOrDefaultAsync(d => d.Id == id); /// <summary> Incluir la lista de empleados en la consulta, para que se devuelvan los empleados del departamento. </summary>
            if (departamento == null)
            {
                return NotFound();
            }
            return Ok(departamento.Empleado);

        }
        /// <summary>
        /// Crea un nuevo departamento en la base de datos. POST: api/Departamento
        /// </summary>
        /// <param name="departamento">El departamento a crear.</param>
        /// <returns>Un resultado de acción que contiene el departamento creado, o una respuesta 201 Created que lo identifica.</returns>
        [HttpPost]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento) 
        {
            _context.Departamento.Add(departamento);/// <summary> Agregar el departamento a la base de datos.</summary>
            await _context.SaveChangesAsync();/// <summary> Guardar los cambios en la base de datos de manera asincrona.</summary>
            return CreatedAtAction(nameof(GetDepartamento), new { id = departamento.Id }, departamento);/// <summary> Devolver una respuesta 201 Created que contiene el departamento creado y la ruta para acceder a él.</summary>
        }
        /// <summary>
        /// Actualiza un departamento existente.
        /// </summary>
        /// <remarks>Si se produce un conflicto de concurrencia y el departamento ya no existe, devuelve
        /// una respuesta NotFound.</remarks>
        /// <param name="id">Identificador del departamento que se va a actualizar.</param>
        /// <param name="departamento">Datos del departamento que se va a actualizar.</param>
        /// <returns>Una respuesta sin contenido si la actualización se realiza correctamente; BadRequest si los identificadores
        /// no coinciden o NotFound si el departamento no existe.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, Departamento departamento)
        {
            if (id != departamento.Id)/// <summary> Verificar que el identificador del departamento en la ruta coincide con el identificador del departamento en el cuerpo de la solicitud.</summary>
            {
                return BadRequest();/// <summary> Devolver una respuesta BadRequest si los identificadores no coinciden.</summary>
            }
            _context.Entry(departamento).State = EntityState.Modified;/// <summary> Marcar el departamento como modificado en el contexto de la base de datos.</summary>
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)/// <summary> Capturar la excepción de concurrencia que se produce cuando se intenta actualizar un departamento que ya no existe en la base de datos.</summary>
            {
                if (!DepartamentoExists(id))
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
        /// Elimina un departamento existente de la base de datos. DELETE: api/Departamento/5
        /// </summary>
        /// <param name="id">El identificador del departamento a eliminar.</param>
        /// <returns>No hay contenido cuando la eliminación tiene éxito; de lo contrario, no encontrado cuando el departamento no existe.|</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)// crear un metodo asincrono que elimine un departamento
        {
            var departamento = await _context.Departamento.FindAsync(id);/// <summary> Buscar el departamento en la base de datos por su identificador.</summary>
            if (departamento == null)
            {
                return NotFound();
            }
            _context.Departamento.Remove(departamento);/// <summary> Eliminar el departamento de la base de datos.</summary>
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool DepartamentoExists(int id) /// <summary> Verifica si un departamento existe en la base de datos por su identificador.</summary>
        {
            return _context.Departamento.Any(e => e.Id == id); /// <summary> Devolver true si el departamento existe, false si no.</summary>
        }
    }
}