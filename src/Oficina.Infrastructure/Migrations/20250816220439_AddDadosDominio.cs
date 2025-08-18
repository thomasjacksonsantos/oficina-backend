using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDadosDominio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- ContaStatus
                if (not exists(select top 1 1 from DadoDominio where Id = '019606a2-1b00-704e-a85d-f33f15a4e992'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('019606a2-1b00-704e-a85d-f33f15a4e992', 'Ativo', 'Ativo', 'ContaStatus')
                end

                if (not exists(select top 1 1 from DadoDominio where Id = '019606a2-5550-7c9d-9939-2eef3e1c5fda'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('019606a2-5550-7c9d-9939-2eef3e1c5fda', 'Inativo', 'Inativo', 'ContaStatus')
                end

                -- TipoTelefone
                if (not exists(select top 1 1 from DadoDominio where Id = '67c3c783-654b-44c5-a07f-24a8b6294709'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('67c3c783-654b-44c5-a07f-24a8b6294709', 'Telefone', 'Telefone', 'TipoTelefone')
                end

                if (not exists(select top 1 1 from DadoDominio where Id = 'c99a5988-b022-4b68-b640-af5021dc7c8f'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('c99a5988-b022-4b68-b640-af5021dc7c8f', 'Celular', 'Celular', 'TipoTelefone')
                end

                -- TipoDocumento
                if (not exists(select top 1 1 from DadoDominio where Id = '10132080-452d-4d7d-861e-0c5801c94d57'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('10132080-452d-4d7d-861e-0c5801c94d57', 'Cpf', 'Cpf', 'TipoDocumento')
                end

                if (not exists(select top 1 1 from DadoDominio where Id = '258f6580-0046-4b1e-9884-8757da63a48e'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('258f6580-0046-4b1e-9884-8757da63a48e', 'Cnpj', 'Cnpj', 'TipoDocumento')
                end

                if (not exists(select top 1 1 from DadoDominio where Id = '0c4df770-8401-4dc7-aebb-338b70ac349a'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('0c4df770-8401-4dc7-aebb-338b70ac349a', 'Rg', 'Rg', 'TipoDocumento')
                end

                -- Sexo
                if (not exists(select top 1 1 from DadoDominio where Id = '01963629-5d16-75e4-a596-295d8ccd46fa'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('01963629-5d16-75e4-a596-295d8ccd46fa', 'Masculino', 'Masculino', 'Sexo')
                end

                if (not exists(select top 1 1 from DadoDominio where Id = '01963629-7cfa-7cb2-9607-c4ac227dda6c'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('01963629-7cfa-7cb2-9607-c4ac227dda6c', 'Feminino', 'Feminino', 'Sexo')
                end

                -- TipoUsuario
                if (not exists(select top 1 1 from DadoDominio where Id = '019606aa-ab94-75ac-9e99-86038e9a66a0'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('019606aa-ab94-75ac-9e99-86038e9a66a0', 'SuperAdmin', 'SuperAdmin', 'TipoUsuario')
                end

                if (not exists(select top 1 1 from DadoDominio where Id = '019606aa-cb80-7529-8187-089720b6c556'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('019606aa-cb80-7529-8187-089720b6c556', 'Funcionario', 'Funcionario', 'TipoUsuario')
                end

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
