using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anotacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtAnotacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dsMensagem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idPessoa = table.Column<int>(type: "int", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anotacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdensServicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nrOrdem = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    nmCliente = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    nrTelefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    flSituacao = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    dsFormaPagamento = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    dsEndereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    flEntrega = table.Column<bool>(type: "bit", nullable: false),
                    dtOrdem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idFuncionario = table.Column<int>(type: "int", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensServicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmPessoa = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    nrCPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    nrRG = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    dsEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nrTelefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    flSituacao = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    nrPIS = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    flEntrega = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dtAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anotacao");

            migrationBuilder.DropTable(
                name: "OrdensServicos");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
