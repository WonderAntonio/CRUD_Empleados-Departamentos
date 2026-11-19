
namespace CRUD_D_E.Data

{
    /// <summary>
    /// Representa un departamento.
    /// </summary>
    /// <param name="Id">El identificador único del departamento.</param>
    /// <param name="Nombre">El nombre del departamento.</param>
    /// <param name="Empleado">La lista de empleados que pertenecen al departamento.</param>
    public class Departamento
    {
        public int Id { get; set; }

        public String? Nombre { get; set; }

        public List<Empleado> Empleado { get; set; } = new();
    }
}
