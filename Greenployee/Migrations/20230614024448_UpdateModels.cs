using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_Funcionarioid",
                table: "OrdensServicos");

            migrationBuilder.DropIndex(
                name: "IX_OrdensServicos_Funcionarioid",
                table: "OrdensServicos");

            migrationBuilder.DropColumn(
                name: "flEntrega",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Funcionarioid",
                table: "OrdensServicos");

            migrationBuilder.DropColumn(
                name: "dtOrdem",
                table: "OrdensServicos");

            migrationBuilder.AlterColumn<string>(
                name: "nrPIS",
                table: "Pessoas",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dtAdmissao",
                table: "Pessoas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "idMeta",
                table: "PessoaMetas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idPessoa",
                table: "PessoaMetas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "nrTelefone",
                table: "OrdensServicos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "nmCliente",
                table: "OrdensServicos",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<int>(
                name: "idFuncionario",
                table: "OrdensServicos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "flSituacao",
                table: "OrdensServicos",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "dsFormaPagamento",
                table: "OrdensServicos",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "dsEndereco",
                table: "OrdensServicos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<decimal>(
                name: "vlTotal",
                table: "OrdensServicos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "vlUnitario",
                table: "OrdemServicoItens",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idOrdemServico",
                table: "OrdemServicoItens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "flConcluido",
                table: "Anotacoes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_idUsuario",
                table: "Pessoas",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaMetas_idMeta",
                table: "PessoaMetas",
                column: "idMeta");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaMetas_idPessoa",
                table: "PessoaMetas",
                column: "idPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensServicos_idFuncionario",
                table: "OrdensServicos",
                column: "idFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServicoItens_idOrdemServico",
                table: "OrdemServicoItens",
                column: "idOrdemServico");

            migrationBuilder.CreateIndex(
                name: "IX_Anotacoes_idPessoa",
                table: "Anotacoes",
                column: "idPessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Anotacoes_Pessoas_idPessoa",
                table: "Anotacoes",
                column: "idPessoa",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServicoItens_OrdensServicos_idOrdemServico",
                table: "OrdemServicoItens",
                column: "idOrdemServico",
                principalTable: "OrdensServicos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServicos_Pessoas_idFuncionario",
                table: "OrdensServicos",
                column: "idFuncionario",
                principalTable: "Pessoas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaMetas_Metas_idMeta",
                table: "PessoaMetas",
                column: "idMeta",
                principalTable: "Metas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaMetas_Pessoas_idPessoa",
                table: "PessoaMetas",
                column: "idPessoa",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_idUsuario",
                table: "Pessoas",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anotacoes_Pessoas_idPessoa",
                table: "Anotacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServicoItens_OrdensServicos_idOrdemServico",
                table: "OrdemServicoItens");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_idFuncionario",
                table: "OrdensServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaMetas_Metas_idMeta",
                table: "PessoaMetas");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaMetas_Pessoas_idPessoa",
                table: "PessoaMetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_idUsuario",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_idUsuario",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_PessoaMetas_idMeta",
                table: "PessoaMetas");

            migrationBuilder.DropIndex(
                name: "IX_PessoaMetas_idPessoa",
                table: "PessoaMetas");

            migrationBuilder.DropIndex(
                name: "IX_OrdensServicos_idFuncionario",
                table: "OrdensServicos");

            migrationBuilder.DropIndex(
                name: "IX_OrdemServicoItens_idOrdemServico",
                table: "OrdemServicoItens");

            migrationBuilder.DropIndex(
                name: "IX_Anotacoes_idPessoa",
                table: "Anotacoes");

            migrationBuilder.DropColumn(
                name: "idMeta",
                table: "PessoaMetas");

            migrationBuilder.DropColumn(
                name: "idPessoa",
                table: "PessoaMetas");

            migrationBuilder.DropColumn(
                name: "vlTotal",
                table: "OrdensServicos");

            migrationBuilder.DropColumn(
                name: "idOrdemServico",
                table: "OrdemServicoItens");

            migrationBuilder.AlterColumn<string>(
                name: "nrPIS",
                table: "Pessoas",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dtAdmissao",
                table: "Pessoas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "flEntrega",
                table: "Pessoas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "nrTelefone",
                table: "OrdensServicos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nmCliente",
                table: "OrdensServicos",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idFuncionario",
                table: "OrdensServicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "flSituacao",
                table: "OrdensServicos",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dsFormaPagamento",
                table: "OrdensServicos",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dsEndereco",
                table: "OrdensServicos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Funcionarioid",
                table: "OrdensServicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dtOrdem",
                table: "OrdensServicos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "vlUnitario",
                table: "OrdemServicoItens",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "flConcluido",
                table: "Anotacoes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdensServicos_Funcionarioid",
                table: "OrdensServicos",
                column: "Funcionarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServicos_Pessoas_Funcionarioid",
                table: "OrdensServicos",
                column: "Funcionarioid",
                principalTable: "Pessoas",
                principalColumn: "id");
        }
    }
}
