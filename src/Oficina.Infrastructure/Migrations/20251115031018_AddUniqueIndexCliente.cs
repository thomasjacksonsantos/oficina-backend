using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_TipoDocumentoId",
                table: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_TipoDocumentoId",
                table: "Cliente",
                column: "TipoDocumentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_TipoDocumentoId",
                table: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente",
                column: "SexoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_TipoDocumentoId",
                table: "Cliente",
                column: "TipoDocumentoId",
                unique: true);
        }
    }
}
