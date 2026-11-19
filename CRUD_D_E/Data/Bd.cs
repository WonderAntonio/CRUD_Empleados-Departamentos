using System;
using Microsoft.EntityFrameworkCore;

namespace CRUD_D_E.Data
{

    /// <summary>
    /// Representa la base de datos y sus entidades.
    /// </summary>
    /// Hereda de DbContext para definir la base de datos y sus entidades, y configurar las relaciones entre ellas.
    public class Bd : DbContext 
    {
        /// <summary>
        /// Tabla de empleados en la base de datos.
        /// </summary>
        public DbSet<Empleado> Empleado => Set<Empleado>();
        /// <summary>
        /// Tabla de departamentos en la base de datos.
        /// </summary>
        public DbSet<Departamento> Departamento => Set<Departamento>();
        /// <summary>
        /// Constructor de la clase Bd que recibe las opciones de configuración de la base de datos. inyección de dependencias para poder configurar la base de datos desde el Program.cs
        /// </summary>
        /// <param name="options"> Las opciones de configuración de la base de datos.</param>
        public Bd(DbContextOptions<Bd> options) : base(options) { } 
        /// <summary>
        /// Configura las entidades Empleado y Departamento, sus claves, propiedades y relaciones.
        /// </summary>
        /// <param name="modelBuilder">El generador utilizado para configurar el modelo de Entity Framework.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            /// <summary>
            /// Configura la entidad Empleado.
            /// </summary>
            /// <param name="ent">El builder para configurar la entidad Empleado.</param>
            /// <param name="modelBuilder">El generador utilizado para configurar el modelo de Entity Framework.</param>
            modelBuilder.Entity<Empleado>(ent => 
            {
                ent.HasKey(e => e.Id); /// <summary> Define la llave primaria de la entidad Empleado.
                ent.Property(e => e.Id).ValueGeneratedOnAdd(); /// <summary> Define que el ID se genera automáticamente.
                ent.Property(e => e.Nombre).IsRequired(false); /// <summary> Define que el nombre puede ser nulo.
                ent.Property(e => e.Salario).HasColumnType("decimal(18,2)"); /// <summary> Define el tipo de dato del salario.
                ent.HasOne(e => e.Departamento) /// <summary> Define la relación con Departamento, un empleado pertenece a un departamento, puede ser nulo si no tiene departamento asignado.
                    .WithMany(d => d.Empleado) /// <summary> Un departamento puede tener muchos empleados.

                    .HasForeignKey(e => e.IdDepartamento) /// <summary> Define la llave foranea
                    .OnDelete(DeleteBehavior.SetNull); /// <summary> Si se elimina un departamento, los empleados quedan sin departamento asignado (null)
            });
            /// <summary>
            /// Configura la entidad Departamento.
            /// </summary>
            /// <param name="ent">El builder para configurar la entidad Departamento.</param>
            /// <param name="modelBuilder">El generador utilizado para configurar el modelo de Entity Framework.</param>
            modelBuilder.Entity<Departamento>(ent => 
            {
                ent.HasKey(d => d.Id); /// <summary> Define la llave primaria.
                ent.Property(d => d.Nombre).IsRequired(false); /// <summary> Define que el nombre puede ser nulo.
            });
        }
    }
}
