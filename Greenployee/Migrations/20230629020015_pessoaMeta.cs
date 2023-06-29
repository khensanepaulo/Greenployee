using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class pessoaMeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dtConcluido",
                table: "Metas");

            migrationBuilder.AddColumn<DateTime>(
                name: "dtConcluido",
                table: "PessoaMetas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "flConcluido",
                table: "PessoaMetas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "vlAlcancado",
                table: "PessoaMetas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dtConcluido",
                table: "PessoaMetas");

            migrationBuilder.DropColumn(
                name: "flConcluido",
                table: "PessoaMetas");

            migrationBuilder.DropColumn(
                name: "vlAlcancado",
                table: "PessoaMetas");

            migrationBuilder.AddColumn<DateTime>(
                name: "dtConcluido",
                table: "Metas",
                type: "datetime2",
                nullable: true);
        }
    }
}
