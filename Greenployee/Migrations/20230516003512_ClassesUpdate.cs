using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class ClassesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_Funcionarioid",
                table: "OrdensServicos");

            migrationBuilder.DropColumn(
                name: "idFuncionario",
                table: "OrdensServicos");

            migrationBuilder.RenameColumn(
                name: "Funcionarioid",
                table: "OrdensServicos",
                newName: "FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensServicos_Funcionarioid",
                table: "OrdensServicos",
                newName: "IX_OrdensServicos_FuncionarioId");

            migrationBuilder.AddColumn<int>(
                name: "idUsuario",
                table: "Pessoas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioId",
                table: "OrdensServicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dsRecompensa",
                table: "Metas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "dsMensagem",
                table: "Anotacoes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServicos_Pessoas_FuncionarioId",
                table: "OrdensServicos",
                column: "FuncionarioId",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensServicos_Pessoas_FuncionarioId",
                table: "OrdensServicos");

            migrationBuilder.DropColumn(
                name: "idUsuario",
                table: "Pessoas");

            migrationBuilder.RenameColumn(
                name: "FuncionarioId",
                table: "OrdensServicos",
                newName: "Funcionarioid");

            migrationBuilder.RenameIndex(
                name: "IX_OrdensServicos_FuncionarioId",
                table: "OrdensServicos",
                newName: "IX_OrdensServicos_Funcionarioid");

            migrationBuilder.AlterColumn<int>(
                name: "Funcionarioid",
                table: "OrdensServicos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "idFuncionario",
                table: "OrdensServicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "dsRecompensa",
                table: "Metas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "dsMensagem",
                table: "Anotacoes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensServicos_Pessoas_Funcionarioid",
                table: "OrdensServicos",
                column: "Funcionarioid",
                principalTable: "Pessoas",
                principalColumn: "id");
        }
    }
}
