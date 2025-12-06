using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGrupoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrupoProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    NCM = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    ANP = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    GrupoProdutoStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoProduto_DadoDominio_GrupoProdutoStatusId",
                        column: x => x.GrupoProdutoStatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoProduto_GrupoProdutoStatusId",
                table: "GrupoProduto",
                column: "GrupoProdutoStatusId");

            migrationBuilder.Sql(@"
                -- GrupoProdutoStatus Ativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '0865836b-7a45-4f85-9c7e-5a7ac6e65e55'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('0865836b-7a45-4f85-9c7e-5a7ac6e65e55', 'Ativo', 'Ativo', 'GrupoProdutoStatus')
                END

                -- GrupoProdutoStatus Inativo
                IF (NOT EXISTS(SELECT TOP 1 1 FROM DadoDominio WHERE Id = '9f8b5e8c-6e7d-4ea4-9f2d-3be51f6b9cd2'))
                BEGIN
                    INSERT INTO dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    VALUES('9f8b5e8c-6e7d-4ea4-9f2d-3be51f6b9cd2', 'Inativo', 'Inativo', 'GrupoProdutoStatus')
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoProduto");
        }
    }
}
