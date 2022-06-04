using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education.Persistence.Migrations
{
    public partial class EducationMigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoId);
                });

            migrationBuilder.InsertData(
                table: "Curso",
                columns: new[] { "CursoId", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[] { new Guid("0ec9ac4e-5f04-4d3c-be03-94dfea4ca5a7"), "Curso de C# básico", new DateTime(2022, 6, 2, 19, 10, 47, 127, DateTimeKind.Local).AddTicks(6418), new DateTime(2024, 6, 2, 19, 10, 47, 127, DateTimeKind.Local).AddTicks(6431), 56m, "C# desde cero hasta avanzado" });

            migrationBuilder.InsertData(
                table: "Curso",
                columns: new[] { "CursoId", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[] { new Guid("1595c01e-5300-4239-85ea-604ca757da13"), "Master en Unit Test con CQRS", new DateTime(2022, 6, 2, 19, 10, 47, 127, DateTimeKind.Local).AddTicks(6441), new DateTime(2024, 6, 2, 19, 10, 47, 127, DateTimeKind.Local).AddTicks(6442), 1000m, "Curso de Unit Test para NET Core" });

            migrationBuilder.InsertData(
                table: "Curso",
                columns: new[] { "CursoId", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[] { new Guid("db4f0e44-ae21-4259-8bef-92d3cef38dca"), "Master en Java Spring desde las raices", new DateTime(2022, 6, 2, 19, 10, 47, 127, DateTimeKind.Local).AddTicks(6438), new DateTime(2024, 6, 2, 19, 10, 47, 127, DateTimeKind.Local).AddTicks(6439), 25m, "Curso de Java" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curso");
        }
    }
}
