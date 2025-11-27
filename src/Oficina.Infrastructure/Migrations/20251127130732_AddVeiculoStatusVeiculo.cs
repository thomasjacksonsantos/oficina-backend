using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVeiculoStatusVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VeiculoStatusId",
                table: "Veiculo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@"
                -- Veiculo Status Ativo
                if (not exists(select top 1 1 from DadoDominio where Id = '9cc10d5a-0355-496b-a116-537c9aaffbdb'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('9cc10d5a-0355-496b-a116-537c9aaffbdb', 'Ativo', 'Ativo', 'VeiculoStatus')
                end

                -- Veiculo Status Inativo
                if (not exists(select top 1 1 from DadoDominio where Id = '632fa768-d5e4-455c-a0f3-85272af95b39'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('632fa768-d5e4-455c-a0f3-85272af95b39', 'Inativo', 'Inativo', 'VeiculoStatus')
                end
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VeiculoStatusId",
                table: "Veiculo");
        }
    }
}
