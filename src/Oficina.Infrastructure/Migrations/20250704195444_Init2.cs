using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "Enderecos",
                table: "Usuario",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Usuario",
                newName: "Enderecos");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
