using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeSexoJsonSerializer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Cliente");

            migrationBuilder.AddColumn<Guid>(
                name: "SexoId",
                table: "Usuario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SexoId",
                table: "Cliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_SexoId",
                table: "Usuario",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente",
                column: "SexoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_DadoDominio_SexoId",
                table: "Cliente",
                column: "SexoId",
                principalTable: "DadoDominio",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_DadoDominio_SexoId",
                table: "Usuario",
                column: "SexoId",
                principalTable: "DadoDominio",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_DadoDominio_SexoId",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_DadoDominio_SexoId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_SexoId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "SexoId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "SexoId",
                table: "Cliente");

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
