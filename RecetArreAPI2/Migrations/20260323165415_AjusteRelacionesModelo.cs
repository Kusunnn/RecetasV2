using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecetArreAPI2.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionesModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_AspNetUsers_AutorId",
                table: "Recetas");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_CreadoUtc",
                table: "Comentarios",
                column: "CreadoUtc");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_AspNetUsers_AutorId",
                table: "Recetas",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_AspNetUsers_AutorId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_CreadoUtc",
                table: "Comentarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_AspNetUsers_AutorId",
                table: "Recetas",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
