using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class idOrdemServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServicoItens_OrdensServicos_idOrdemServico",
                table: "OrdemServicoItens");

            migrationBuilder.AlterColumn<int>(
                name: "idOrdemServico",
                table: "OrdemServicoItens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServicoItens_OrdensServicos_idOrdemServico",
                table: "OrdemServicoItens",
                column: "idOrdemServico",
                principalTable: "OrdensServicos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServicoItens_OrdensServicos_idOrdemServico",
                table: "OrdemServicoItens");

            migrationBuilder.AlterColumn<int>(
                name: "idOrdemServico",
                table: "OrdemServicoItens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServicoItens_OrdensServicos_idOrdemServico",
                table: "OrdemServicoItens",
                column: "idOrdemServico",
                principalTable: "OrdensServicos",
                principalColumn: "id");
        }
    }
}
