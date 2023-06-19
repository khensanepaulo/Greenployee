using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenployee.API.Migrations
{
    /// <inheritdoc />
    public partial class StartDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dsRecompensa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    dtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vlMeta = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmVisual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nmPermissao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dsLogin = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    dsSenha = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PermissaoUsuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idPermissao = table.Column<int>(type: "int", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoUsuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_PermissaoUsuarios_Permissoes_idPermissao",
                        column: x => x.idPermissao,
                        principalTable: "Permissoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissaoUsuarios_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmPessoa = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    nrCPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    nrRG = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    dsEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nrTelefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    flSituacao = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    nrPIS = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    dtAdmissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idUsuario = table.Column<int>(type: "int", nullable: true),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pessoas_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Anotacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dsMensagem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    flConcluido = table.Column<bool>(type: "bit", nullable: true),
                    idPessoa = table.Column<int>(type: "int", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anotacoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Anotacoes_Pessoas_idPessoa",
                        column: x => x.idPessoa,
                        principalTable: "Pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdensServicos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nrOrdem = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    nmCliente = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    nrTelefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    flSituacao = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    dsFormaPagamento = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    dsEndereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    flEntrega = table.Column<bool>(type: "bit", nullable: false),
                    vlTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    idFuncionario = table.Column<int>(type: "int", nullable: true),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensServicos", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrdensServicos_Pessoas_idFuncionario",
                        column: x => x.idFuncionario,
                        principalTable: "Pessoas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PessoaMetas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPessoa = table.Column<int>(type: "int", nullable: false),
                    idMeta = table.Column<int>(type: "int", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaMetas", x => x.id);
                    table.ForeignKey(
                        name: "FK_PessoaMetas_Metas_idMeta",
                        column: x => x.idMeta,
                        principalTable: "Metas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PessoaMetas_Pessoas_idPessoa",
                        column: x => x.idPessoa,
                        principalTable: "Pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdemServicoItens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmProduto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    vlUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nrQuantidade = table.Column<int>(type: "int", nullable: false),
                    idOrdemServico = table.Column<int>(type: "int", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtAtualizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dtExcluido = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServicoItens", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrdemServicoItens_OrdensServicos_idOrdemServico",
                        column: x => x.idOrdemServico,
                        principalTable: "OrdensServicos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anotacoes_idPessoa",
                table: "Anotacoes",
                column: "idPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServicoItens_idOrdemServico",
                table: "OrdemServicoItens",
                column: "idOrdemServico");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensServicos_idFuncionario",
                table: "OrdensServicos",
                column: "idFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoUsuarios_idPermissao",
                table: "PermissaoUsuarios",
                column: "idPermissao");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoUsuarios_idUsuario",
                table: "PermissaoUsuarios",
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
                name: "IX_Pessoas_idUsuario",
                table: "Pessoas",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anotacoes");

            migrationBuilder.DropTable(
                name: "OrdemServicoItens");

            migrationBuilder.DropTable(
                name: "PermissaoUsuarios");

            migrationBuilder.DropTable(
                name: "PessoaMetas");

            migrationBuilder.DropTable(
                name: "OrdensServicos");

            migrationBuilder.DropTable(
                name: "Permissoes");

            migrationBuilder.DropTable(
                name: "Metas");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
