using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVeiculoStatusForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_VeiculoStatusId",
                table: "Veiculo",
                column: "VeiculoStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_DadoDominio_VeiculoStatusId",
                table: "Veiculo",
                column: "VeiculoStatusId",
                principalTable: "DadoDominio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_DadoDominio_VeiculoStatusId",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_VeiculoStatusId",
                table: "Veiculo");
        }
    }
}
