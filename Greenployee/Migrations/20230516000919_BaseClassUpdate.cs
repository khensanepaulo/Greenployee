using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class BaseClassUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_FuncionarioId",
                table: "OrdensServicos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pessoas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "FuncionarioId",
                table: "OrdensServicos",
                newName: "Funcionarioid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrdensServicos",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensServicos_FuncionarioId",
                table: "OrdensServicos",
                newName: "IX_OrdensServicos_Funcionarioid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Metas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Anotacoes",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServicos_Pessoas_Funcionarioid",
                table: "OrdensServicos",
                column: "Funcionarioid",
                principalTable: "Pessoas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_Funcionarioid",
                table: "OrdensServicos");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pessoas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Funcionarioid",
                table: "OrdensServicos",
                newName: "FuncionarioId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrdensServicos",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensServicos_Funcionarioid",
                table: "OrdensServicos",
                newName: "IX_OrdensServicos_FuncionarioId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Metas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Anotacoes",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServicos_Pessoas_FuncionarioId",
                table: "OrdensServicos",
                column: "FuncionarioId",
                principalTable: "Pessoas",
                principalColumn: "Id");
        }
    }
}
