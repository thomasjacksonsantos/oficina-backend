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
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

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
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hodrometro = table.Column<int>(type: "int", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    NumeroChassi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Motorizacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chassi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    SexoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contatos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enderecos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email_Principal = table.Column<bool>(type: "bit", nullable: false),
                    Principal = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_DadoDominio_SexoId",
                        column: x => x.SexoId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cliente_DadoDominio_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
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
                name: "OrdemServicoPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroParcela = table.Column<int>(type: "int", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServicoPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemServicoPagamento_DadoDominio_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
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
                name: "PrecoLoja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecoLoja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecoLoja_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculoCliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeiculoCliente_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFantasia = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    InscricaoEstadual = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Site = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    LogoTipo = table.Column<string>(type: "text", nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contatos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loja_Conta_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loja_DadoDominio_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "DadoDominio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                name: "FuncionarioExecutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioExecutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionarioExecutor_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrdemServico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataFaturamentoInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFaturamentoFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FuncionarioExecutorId = table.Column<int>(type: "int", nullable: false),
                    VeiculoClienteId = table.Column<int>(type: "int", nullable: false),
                    PagamentoId = table.Column<int>(type: "int", nullable: true),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemServico_FuncionarioExecutor_FuncionarioExecutorId",
                        column: x => x.FuncionarioExecutorId,
                        principalTable: "FuncionarioExecutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdemServico_OrdemServicoPagamento_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "OrdemServicoPagamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdemServico_VeiculoCliente_VeiculoClienteId",
                        column: x => x.VeiculoClienteId,
                        principalTable: "VeiculoCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoServicoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorLiquido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrdemServicoId = table.Column<int>(type: "int", nullable: true),
                    Atualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Criado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoServicoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoServicoItem_OrdemServico_OrdemServicoId",
                        column: x => x.OrdemServicoId,
                        principalTable: "OrdemServico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_SexoId",
                table: "Cliente",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_TipoDocumentoId",
                table: "Cliente",
                column: "TipoDocumentoId");

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
                name: "IX_FuncionarioExecutor_UsuarioId",
                table: "FuncionarioExecutor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Loja_ContaId",
                table: "Loja",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Loja_TipoDocumentoId",
                table: "Loja",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServico_FuncionarioExecutorId",
                table: "OrdemServico",
                column: "FuncionarioExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServico_PagamentoId",
                table: "OrdemServico",
                column: "PagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServico_VeiculoClienteId",
                table: "OrdemServico",
                column: "VeiculoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServicoPagamento_TipoPagamentoId",
                table: "OrdemServicoPagamento",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PrecoLoja_ProdutoId",
                table: "PrecoLoja",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoServicoItem_OrdemServicoId",
                table: "ProdutoServicoItem",
                column: "OrdemServicoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoCliente_ClienteId",
                table: "VeiculoCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoCliente_VeiculoId",
                table: "VeiculoCliente",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaUsuario");

            migrationBuilder.DropTable(
                name: "PrecoLoja");

            migrationBuilder.DropTable(
                name: "ProdutoServicoItem");

            migrationBuilder.DropTable(
                name: "UsuarioLoja");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "OrdemServico");

            migrationBuilder.DropTable(
                name: "Loja");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "FuncionarioExecutor");

            migrationBuilder.DropTable(
                name: "OrdemServicoPagamento");

            migrationBuilder.DropTable(
                name: "VeiculoCliente");

            migrationBuilder.DropTable(
                name: "Conta");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "DadoDominio");
        }
    }
}
