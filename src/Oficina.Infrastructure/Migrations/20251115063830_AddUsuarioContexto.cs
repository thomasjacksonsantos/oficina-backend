using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioContexto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioContexto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    LojaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioContexto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioContexto_Conta_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Conta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioContexto_Loja_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Loja",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioContexto_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioContexto_ContaId",
                table: "UsuarioContexto",
                column: "ContaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioContexto_LojaId",
                table: "UsuarioContexto",
                column: "LojaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioContexto_UsuarioId",
                table: "UsuarioContexto",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioContexto");
        }
    }
}
