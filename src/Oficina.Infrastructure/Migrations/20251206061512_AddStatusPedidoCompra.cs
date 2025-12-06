using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusPedidoCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusPedidoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    StatusPedidoCompraStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPedidoCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusPedidoCompra_DadoDominio_StatusPedidoCompraStatusId",
                        column: x => x.StatusPedidoCompraStatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusPedidoCompra_StatusPedidoCompraStatusId",
                table: "StatusPedidoCompra",
                column: "StatusPedidoCompraStatusId");

            migrationBuilder.Sql(@"
                -- StatusPedidoCompraStatus Ativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '2b8e4c1a-7d3f-4a6b-9c2e-5f1b8a3c6d7e'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('2b8e4c1a-7d3f-4a6b-9c2e-5f1b8a3c6d7e', 'Ativo', 'Ativo', 'StatusPedidoCompraStatus')
                END

                -- StatusPedidoCompraStatus Inativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '6a1c3e2b-9d7f-4c8a-8b2e-3d5f7a1b2c4e'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('6a1c3e2b-9d7f-4c8a-8b2e-3d5f7a1b2c4e', 'Inativo', 'Inativo', 'StatusPedidoCompraStatus')
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusPedidoCompra");
        }
    }
}
