using System.Numerics;

namespace CRUD_D_E.Data
{
    /// <summary>
    /// Especifica el estado actual de un empleado.
    /// </summary>
    public enum EstatusEmpleado
    {
        Activo = 1,
        Inactivo = 0
    }
    /// <summary>
    /// Representa a un empleado.
    /// </summary>
    ///<param name="Id">El identificador único del empleado.</param>
    ///<param name="Nombre">El nombre del empleado.</param>
    ///<param name="IdDepartamento">El identificador del departamento al que pertenece el empleado.</param>
    ///<param name="Salario">El salario del empleado.</param>
    ///<param name="Estatus">El estado actual del empleado.</param>

    public class Empleado
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdDepartamento { get; set; }
        public decimal Salario { get; set; }
        public EstatusEmpleado Estatus { get; set; }
        public Departamento? Departamento { get; set; }
    }
}
