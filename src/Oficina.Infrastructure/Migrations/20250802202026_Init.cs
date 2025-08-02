using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DadoDominio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dominio = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoDominio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Cliente_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente_Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    SexoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contatos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enderecos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Documento_TipoDocumento_Dominio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Documento_TipoDocumento_Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Email_Principal = table.Column<bool>(type: "bit", nullable: false),
                    Principal = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Cliente_Id);
                    table.ForeignKey(
                        name: "FK_Cliente_DadoDominio_SexoId",
                        column: x => x.SexoId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conta_DadoDominio_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SexoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioPadrao = table.Column<bool>(type: "bit", nullable: false),
                    Contatos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoClass = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_DadoDominio_SexoId",
                        column: x => x.SexoId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Usuario_DadoDominio_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Usuario_DadoDominio_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Loja",
                columns: table => new
                {
                    Loja_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loja_Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contatos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Documento_TipoDocumento_Dominio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Documento_TipoDocumento_Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loja", x => x.Loja_Id);
                    table.ForeignKey(
                        name: "FK_Loja_Conta_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContaUsuario",
                columns: table => new
                {
                    ContasId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaUsuario", x => new { x.ContasId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_ContaUsuario_Conta_ContasId",
                        column: x => x.ContasId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContaUsuario_Usuario_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLoja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Criado_Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Atualizado_Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LojaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLoja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioLoja_Loja_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Loja",
                        principalColumn: "Loja_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_StatusId",
                table: "Conta",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaUsuario_UsuariosId",
                table: "ContaUsuario",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_DadoDominio_Key_Dominio",
                table: "DadoDominio",
                columns: new[] { "Key", "Dominio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loja_ContaId",
                table: "Loja",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_SexoId",
                table: "Usuario",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoDocumentoId",
                table: "Usuario",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLoja_LojaId",
                table: "UsuarioLoja",
                column: "LojaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "ContaUsuario");

            migrationBuilder.DropTable(
                name: "UsuarioLoja");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Loja");

            migrationBuilder.DropTable(
                name: "Conta");

            migrationBuilder.DropTable(
                name: "DadoDominio");
        }
    }
}
