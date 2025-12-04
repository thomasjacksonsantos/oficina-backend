using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFornecedorDominio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFantasia = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Site = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    InscricaoMunicipal = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    InscricaoEstadual = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    FornecedorStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoConsumidorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contatos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email_Principal = table.Column<bool>(type: "bit", nullable: false),
                    Principal = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fornecedor_DadoDominio_FornecedorStatusId",
                        column: x => x.FornecedorStatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fornecedor_DadoDominio_TipoConsumidorId",
                        column: x => x.TipoConsumidorId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_FornecedorStatusId",
                table: "Fornecedor",
                column: "FornecedorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_TipoConsumidorId",
                table: "Fornecedor",
                column: "TipoConsumidorId");

            migrationBuilder.Sql(@"
                -- TipoConsumidor ConsumidorFinal
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = 'f3d8f4d2-4fb3-4b8a-9d61-9a3f5f77d3c2'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('f3d8f4d2-4fb3-4b8a-9d61-9a3f5f77d3c2', 'ConsumidorFinal', 'Consumidor Final', 'TipoConsumidor')
                END

                -- TipoConsumidor Revenda
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '0c6a2bb1-6eac-4e21-bb94-2c0d35363f9c'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('0c6a2bb1-6eac-4e21-bb94-2c0d35363f9c', 'Revenda', 'Revenda', 'TipoConsumidor')
                END

                -- FornecedorStatus Ativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '7c4b1f2d-1e6b-4971-9c82-4f9d2a7e3b11'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('7c4b1f2d-1e6b-4971-9c82-4f9d2a7e3b11', 'Ativo', 'Ativo', 'FornecedorStatus')
                END

                -- FornecedorStatus Inativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = 'd2f1a8c3-9b55-4fa7-96f4-8e3c1d77a024'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('d2f1a8c3-9b55-4fa7-96f4-8e3c1d77a024', 'Inativo', 'Inativo', 'FornecedorStatus')
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
