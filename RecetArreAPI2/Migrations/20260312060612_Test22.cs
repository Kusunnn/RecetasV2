using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecetArreAPI2.Migrations
{
    /// <inheritdoc />
    public partial class Test22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_AspNetUsers_IdUsuario",
                table: "Recetas");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Rec_Tiem");

            migrationBuilder.DropTable(
                name: "Tiempos");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_IdUsuario",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Recetas");

            migrationBuilder.AlterColumn<string>(
                name: "Instrucciones",
                table: "Recetas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(3000)",
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreadoUtc",
                table: "Recetas",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "AutorId",
                table: "Recetas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Recetas",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstaPublicado",
                table: "Recetas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoUtc",
                table: "Recetas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Porciones",
                table: "Recetas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TiempoCoccionMinutos",
                table: "Recetas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TiempoPreparacionMinutos",
                table: "Recetas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Recetas",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RecetasId",
                table: "Ingredientes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecetasId",
                table: "Categorias",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_AutorId",
                table: "Recetas",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_RecetasId",
                table: "Ingredientes",
                column: "RecetasId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_RecetasId",
                table: "Categorias",
                column: "RecetasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Recetas_RecetasId",
                table: "Categorias",
                column: "RecetasId",
                principalTable: "Recetas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Recetas_RecetasId",
                table: "Ingredientes",
                column: "RecetasId",
                principalTable: "Recetas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_AspNetUsers_AutorId",
                table: "Recetas",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Recetas_RecetasId",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Recetas_RecetasId",
                table: "Ingredientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_AspNetUsers_AutorId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_AutorId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_RecetasId",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_RecetasId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "EstaPublicado",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "ModificadoUtc",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Porciones",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "TiempoCoccionMinutos",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "TiempoPreparacionMinutos",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "RecetasId",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "RecetasId",
                table: "Categorias");

            migrationBuilder.AlterColumn<string>(
                name: "Instrucciones",
                table: "Recetas",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreadoUtc",
                table: "Recetas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "Recetas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Recetas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdReceta = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<string>(type: "text", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Puntuacion = table.Column<int>(type: "integer", nullable: false),
                    TextoCom = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Comentarios_Recetas_IdReceta",
                        column: x => x.IdReceta,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tiempos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiempos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rec_Tiem",
                columns: table => new
                {
                    IdReceta = table.Column<int>(type: "integer", nullable: false),
                    IdTiempo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rec_Tiem", x => new { x.IdReceta, x.IdTiempo });
                    table.ForeignKey(
                        name: "FK_Rec_Tiem_Recetas_IdReceta",
                        column: x => x.IdReceta,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rec_Tiem_Tiempos_IdTiempo",
                        column: x => x.IdTiempo,
                        principalTable: "Tiempos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_IdUsuario",
                table: "Recetas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdReceta",
                table: "Comentarios",
                column: "IdReceta");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuario",
                table: "Comentarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Rec_Tiem_IdTiempo",
                table: "Rec_Tiem",
                column: "IdTiempo");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_AspNetUsers_IdUsuario",
                table: "Recetas",
                column: "IdUsuario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
