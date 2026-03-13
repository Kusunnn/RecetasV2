using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecetArreAPI2.Migrations
{
    /// <inheritdoc />
    public partial class AlignRecetaManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Recetas_RecetasId",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Recetas_RecetasId",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_RecetasId",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_RecetasId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "RecetasId",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "RecetasId",
                table: "Categorias");

            migrationBuilder.AlterColumn<int>(
                name: "Porciones",
                table: "Recetas",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificadoUtc",
                table: "Recetas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Instrucciones",
                table: "Recetas",
                type: "character varying(15000)",
                maxLength: 15000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "EstaPublicado",
                table: "Recetas",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreadoUtc",
                table: "Recetas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Contenido = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreadoUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    RecetaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Recetas_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaCategorias",
                columns: table => new
                {
                    CategoriasId = table.Column<int>(type: "integer", nullable: false),
                    RecetasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaCategorias", x => new { x.CategoriasId, x.RecetasId });
                    table.ForeignKey(
                        name: "FK_RecetaCategorias_Categorias_CategoriasId",
                        column: x => x.CategoriasId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaCategorias_Recetas_RecetasId",
                        column: x => x.RecetasId,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecetaIngredientes",
                columns: table => new
                {
                    IngredientesId = table.Column<int>(type: "integer", nullable: false),
                    RecetasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaIngredientes", x => new { x.IngredientesId, x.RecetasId });
                    table.ForeignKey(
                        name: "FK_RecetaIngredientes_Ingredientes_IngredientesId",
                        column: x => x.IngredientesId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaIngredientes_Recetas_RecetasId",
                        column: x => x.RecetasId,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_CreadoUtc",
                table: "Recetas",
                column: "CreadoUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_EstaPublicado",
                table: "Recetas",
                column: "EstaPublicado");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_RecetaId",
                table: "Comentarios",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaCategorias_RecetasId",
                table: "RecetaCategorias",
                column: "RecetasId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIngredientes_RecetasId",
                table: "RecetaIngredientes",
                column: "RecetasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "RecetaCategorias");

            migrationBuilder.DropTable(
                name: "RecetaIngredientes");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_CreadoUtc",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_EstaPublicado",
                table: "Recetas");

            migrationBuilder.AlterColumn<int>(
                name: "Porciones",
                table: "Recetas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificadoUtc",
                table: "Recetas",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Instrucciones",
                table: "Recetas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15000)",
                oldMaxLength: 15000);

            migrationBuilder.AlterColumn<bool>(
                name: "EstaPublicado",
                table: "Recetas",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreadoUtc",
                table: "Recetas",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

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
        }
    }
}
