using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAreaProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Garantia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaProdutoStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaProduto_DadoDominio_AreaProdutoStatusId",
                        column: x => x.AreaProdutoStatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaProduto_AreaProdutoStatusId",
                table: "AreaProduto",
                column: "AreaProdutoStatusId");

            migrationBuilder.Sql(@"
                -- AreaProdutoStatus Ativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '7e2b1c3a-4f6d-4e2a-9b1a-2c3d4e5f6a7b'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('7e2b1c3a-4f6d-4e2a-9b1a-2c3d4e5f6a7b', 'Ativo', 'Ativo', 'AreaProdutoStatus')
                END

                -- AreaProdutoStatus Inativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '1a9c8b7d-2e3f-4c5a-8b6d-7e1f2a3c4b5d'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('1a9c8b7d-2e3f-4c5a-8b6d-7e1f2a3c4b5d', 'Inativo', 'Inativo', 'AreaProdutoStatus')
                END   
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaProduto");
        }
    }
}
