using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoEF.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("579ca17b-a05b-41e2-94fe-e90db5147823"), null, "Actividades personales", 50 },
                    { new Guid("579ca17b-a05b-41e2-94fe-e90db5147889"), null, "Actividades pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("579ca17b-a05b-41e2-94fe-e90db5147810"), new Guid("579ca17b-a05b-41e2-94fe-e90db5147889"), null, new DateTime(2022, 10, 7, 9, 57, 15, 957, DateTimeKind.Local).AddTicks(8010), 1, "Pago de servicios publicos" },
                    { new Guid("579ca17b-a05b-41e2-94fe-e90db5147811"), new Guid("579ca17b-a05b-41e2-94fe-e90db5147823"), null, new DateTime(2022, 10, 7, 9, 57, 15, 957, DateTimeKind.Local).AddTicks(8022), 0, "Terminar de ver pelicula en netflix" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("579ca17b-a05b-41e2-94fe-e90db5147810"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("579ca17b-a05b-41e2-94fe-e90db5147811"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("579ca17b-a05b-41e2-94fe-e90db5147823"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("579ca17b-a05b-41e2-94fe-e90db5147889"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
