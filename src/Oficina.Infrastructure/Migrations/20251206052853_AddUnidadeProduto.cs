using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnidadeProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnidadeProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UnidadeProdutoStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeProduto_DadoDominio_UnidadeProdutoStatusId",
                        column: x => x.UnidadeProdutoStatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeProduto_UnidadeProdutoStatusId",
                table: "UnidadeProduto",
                column: "UnidadeProdutoStatusId");

            migrationBuilder.Sql(@"
                -- UnidadeProdutoStatus Ativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '3c7e2a1b-8f4d-4c2a-9b7e-5d6a8c1f2b3a'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('3c7e2a1b-8f4d-4c2a-9b7e-5d6a8c1f2b3a', 'Ativo', 'Ativo', 'UnidadeProdutoStatus')
                END

                -- UnidadeProdutoStatus Inativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '9a2b6c4d-1e7f-4b3a-8c2d-3f5e7a9b1c6d'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('9a2b6c4d-1e7f-4b3a-8c2d-3f5e7a9b1c6d', 'Inativo', 'Inativo', 'UnidadeProdutoStatus')
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnidadeProduto");
        }
    }
}
