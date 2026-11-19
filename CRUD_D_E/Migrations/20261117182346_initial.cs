using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_D_E.Migrations;

/// <inheritdoc />
public partial class _20261117182346_initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Departamento",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Departamento", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Empleado",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IdDepartamento = table.Column<int>(type: "int", nullable: true),
                Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Estatus = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Empleado", x => x.Id);
                table.ForeignKey(
                    name: "FK_Empleado_Departamento_IdDepartamento",
                    column: x => x.IdDepartamento,
                    principalTable: "Departamento",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Empleado_IdDepartamento",
            table: "Empleado",
            column: "IdDepartamento");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Empleado");

        migrationBuilder.DropTable(
            name: "Departamento");
    }
}
