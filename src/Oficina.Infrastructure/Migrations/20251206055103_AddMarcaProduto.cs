using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMarcaProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcaProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MarcaProdutoStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarcaProduto_DadoDominio_MarcaProdutoStatusId",
                        column: x => x.MarcaProdutoStatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarcaProduto_MarcaProdutoStatusId",
                table: "MarcaProduto",
                column: "MarcaProdutoStatusId");

            migrationBuilder.Sql(@"
                -- MarcaProdutoStatus Ativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '4e7b2c1a-9d3f-4b6a-8c2e-5f1a7b3c6d8e'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('4e7b2c1a-9d3f-4b6a-8c2e-5f1a7b3c6d8e', 'Ativo', 'Ativo', 'MarcaProdutoStatus')
                END

                -- MarcaProdutoStatus Inativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '8a1c5e2b-3d7f-4c9a-9b2e-6d4f7a1b2c3e'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('8a1c5e2b-3d7f-4c9a-9b2e-6d4f7a1b2c3e', 'Inativo', 'Inativo', 'MarcaProdutoStatus')
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarcaProduto");
        }
    }
}
