using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class ClassesUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdemServicoItens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmProduto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    vlUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nrQuantidade = table.Column<int>(type: "int", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServicoItens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PessoaMetas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaMetas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dsLogin = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    dsSenha = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    tpAcesso = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdemServicoItens");

            migrationBuilder.DropTable(
                name: "PessoaMetas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
