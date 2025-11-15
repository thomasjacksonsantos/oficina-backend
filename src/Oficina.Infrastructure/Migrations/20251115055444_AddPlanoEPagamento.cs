using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanoEPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanoId",
                table: "Conta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PagamentoConta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pago = table.Column<bool>(type: "bit", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentoConta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentoConta_Conta_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LimiteLojas = table.Column<int>(type: "int", nullable: false),
                    PlanoStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plano_DadoDominio_PlanoStatusId",
                        column: x => x.PlanoStatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conta_PlanoId",
                table: "Conta",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentoConta_ContaId",
                table: "PagamentoConta",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_PlanoStatusId",
                table: "Plano",
                column: "PlanoStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Plano_PlanoId",
                table: "Conta",
                column: "PlanoId",
                principalTable: "Plano",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Plano_PlanoId",
                table: "Conta");

            migrationBuilder.DropTable(
                name: "PagamentoConta");

            migrationBuilder.DropTable(
                name: "Plano");

            migrationBuilder.DropIndex(
                name: "IX_Conta_PlanoId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Conta");
        }
    }
}
