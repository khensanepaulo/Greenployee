using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class idsForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_idFuncionario",
                table: "OrdensServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaMetas_Metas_idMeta",
                table: "PessoaMetas");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaMetas_Pessoas_idPessoa",
                table: "PessoaMetas");

            migrationBuilder.AlterColumn<int>(
                name: "idPessoa",
                table: "PessoaMetas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idMeta",
                table: "PessoaMetas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
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

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServicos_Pessoas_idFuncionario",
                table: "OrdensServicos",
                column: "idFuncionario",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_idFuncionario",
                table: "OrdensServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaMetas_Metas_idMeta",
                table: "PessoaMetas");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaMetas_Pessoas_idPessoa",
                table: "PessoaMetas");

            migrationBuilder.AlterColumn<int>(
                name: "idPessoa",
                table: "PessoaMetas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idMeta",
                table: "PessoaMetas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idFuncionario",
                table: "OrdensServicos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaMetas_Pessoas_idPessoa",
                table: "PessoaMetas",
                column: "idPessoa",
                principalTable: "Pessoas",
                principalColumn: "id");
        }
    }
}
