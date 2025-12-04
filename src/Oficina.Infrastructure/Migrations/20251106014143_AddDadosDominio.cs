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
                -- ClienteStatus Ativo
                if (not exists(select top 1 1 from DadoDominio where Id = 'b8e2a1c4-6f3d-4e2b-9a7c-8d2e4f5b1a6c'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('b8e2a1c4-6f3d-4e2b-9a7c-8d2e4f5b1a6c', 'Ativo', 'Ativo', 'ClienteStatus')
                end

                -- ClienteStatus Inativo
                if (not exists(select top 1 1 from DadoDominio where Id = 'c7a3d2e1-5b6f-4c9d-8f0a-3b2c4d5e7f8a'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('c7a3d2e1-5b6f-4c9d-8f0a-3b2c4d5e7f8a', 'Inativo', 'Inativo', 'ClienteStatus')
                end
                
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
                
                if (not exists(select top 1 1 from DadoDominio where Id = 'E2D1A590-5C7C-4F1D-8F1D-3C4F0E5B5E6A'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('E2D1A590-5C7C-4F1D-8F1D-3C4F0E5B5E6A', 'Comercial', 'Comercial', 'TipoTelefone')
                end

                if (not exists(select top 1 1 from DadoDominio where Id = 'A1F5D6E7-3B2C-4D8E-9F0A-1B2C34D5E6F7'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('A1F5D6E7-3B2C-4D8E-9F0A-1B2C34D5E6F7', 'Residencial', 'Residencial', 'TipoTelefone')
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

                -- OrdemServicoTipoPagamento
                -- Credito
                if (not exists(select top 1 1 from DadoDominio where Id = '5635808c-642c-4163-a14c-73b3938d7188'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('5635808c-642c-4163-a14c-73b3938d7188', 'Credito', 'Credito', 'OrdemServicoTipoPagamento')
                end

                -- Debito
                if (not exists(select top 1 1 from DadoDominio where Id = '39839079-5dd7-4009-86ba-980001b45627'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('39839079-5dd7-4009-86ba-980001b45627', 'Debito', 'Debito', 'OrdemServicoTipoPagamento')
                end

                -- OrdemServicoStatus
                -- Aberto
                if (not exists(select top 1 1 from DadoDominio where Id = '1d961034-70e9-4651-9504-2e24d04ce3e0'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('1d961034-70e9-4651-9504-2e24d04ce3e0', 'Aberto', 'Aberto', 'OrdemServicoStatus')
                end

                -- Fechado
                if (not exists(select top 1 1 from DadoDominio where Id = '7019f9f9-72ed-4fc6-88c6-8d125665ff2a'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('7019f9f9-72ed-4fc6-88c6-8d125665ff2a', 'Fechado', 'Fechado', 'OrdemServicoStatus')
                end

                -- Inativo
                if (not exists(select top 1 1 from DadoDominio where Id = '37be3253-ae1c-4650-b0e5-d0047c313e6f'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('37be3253-ae1c-4650-b0e5-d0047c313e6f', 'Inativo', 'Inativo', 'OrdemServicoStatus')
                end

                -- PlanoStatus Ativo
                if (not exists(select top 1 1 from DadoDominio where Id = 'e3b8c6a2-2f4a-4b9d-8e7a-1c2d3e4f5a6b'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('e3b8c6a2-2f4a-4b9d-8e7a-1c2d3e4f5a6b', 'Ativo', 'Ativo', 'Plano')
                end

                -- PlanoStatus Inativo
                if (not exists(select top 1 1 from DadoDominio where Id = 'a7f2d1c3-5b6e-4c8d-9f0a-2b3c4d5e6f7a'))
                begin
                    insert into dbo.DadoDominio (Id, [Key], Nome, Dominio)
                    values('a7f2d1c3-5b6e-4c8d-9f0a-2b3c4d5e6f7a', 'Inativo', 'Inativo', 'Plano')
                end

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

        }
    }
}
