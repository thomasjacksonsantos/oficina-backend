using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterOrdemServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataPrevisao",
                table: "OrdemServico",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrdemServicoStatusId",
                table: "OrdemServico",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServico_OrdemServicoStatusId",
                table: "OrdemServico",
                column: "OrdemServicoStatusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServico_DadoDominio_OrdemServicoStatusId",
                table: "OrdemServico",
                column: "OrdemServicoStatusId",
                principalTable: "DadoDominio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServico_DadoDominio_OrdemServicoStatusId",
                table: "OrdemServico");

            migrationBuilder.DropIndex(
                name: "IX_OrdemServico_OrdemServicoStatusId",
                table: "OrdemServico");

            migrationBuilder.DropColumn(
                name: "DataPrevisao",
                table: "OrdemServico");

            migrationBuilder.DropColumn(
                name: "OrdemServicoStatusId",
                table: "OrdemServico");
        }
    }
}
