using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClienteStatusId",
                table: "Cliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ClienteStatusId",
                table: "Cliente",
                column: "ClienteStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_DadoDominio_ClienteStatusId",
                table: "Cliente",
                column: "ClienteStatusId",
                principalTable: "DadoDominio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_DadoDominio_ClienteStatusId",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_ClienteStatusId",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "ClienteStatusId",
                table: "Cliente");
        }
    }
}
