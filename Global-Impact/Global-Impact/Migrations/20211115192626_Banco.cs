using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Impact.Migrations
{
    public partial class Banco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_Endereco",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<string>(maxLength: 9, nullable: false),
                    Bairro = table.Column<string>(maxLength: 30, nullable: false),
                    Cidade = table.Column<string>(maxLength: 30, nullable: false),
                    UF = table.Column<string>(maxLength: 2, nullable: false),
                    Logradouro = table.Column<string>(maxLength: 40, nullable: false),
                    Numero = table.Column<string>(maxLength: 5, nullable: false),
                    Complemento = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Endereco", x => x.EnderecoId);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 30, nullable: false),
                    Foto = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Item", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Estabelecimento",
                columns: table => new
                {
                    EstabelecimentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 40, nullable: false),
                    Cnpj = table.Column<string>(maxLength: 18, nullable: false),
                    EnderecoId = table.Column<int>(nullable: true),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    Senha = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Estabelecimento", x => x.EstabelecimentoId);
                    table.ForeignKey(
                        name: "FK_Tb_Estabelecimento_Tb_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Tb_Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Ong",
                columns: table => new
                {
                    OngId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 40, nullable: false),
                    Descricao = table.Column<string>(maxLength: 125, nullable: false),
                    EnderecoId = table.Column<int>(nullable: true),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false),
                    Foto = table.Column<string>(maxLength: 1000, nullable: true),
                    Senha = table.Column<string>(maxLength: 25, nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Ong", x => x.OngId);
                    table.ForeignKey(
                        name: "FK_Tb_Ong_Tb_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Tb_Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Doacao",
                columns: table => new
                {
                    DoacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoEstab = table.Column<int>(nullable: false),
                    CodigoOng = table.Column<int>(nullable: false),
                    DataDoacao = table.Column<DateTime>(nullable: false),
                    EstabelecimentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Doacao", x => x.DoacaoId);
                    table.ForeignKey(
                        name: "FK_Tb_Doacao_Tb_Estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Tb_Estabelecimento",
                        principalColumn: "EstabelecimentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tb_DoacaoItem",
                columns: table => new
                {
                    DoacaoId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    DataValidade = table.Column<DateTime>(nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    UnidadeMedida = table.Column<int>(nullable: false),
                    Foto = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_DoacaoItem", x => new { x.DoacaoId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_Tb_DoacaoItem_Tb_Doacao_DoacaoId",
                        column: x => x.DoacaoId,
                        principalTable: "Tb_Doacao",
                        principalColumn: "DoacaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_DoacaoItem_Tb_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Tb_Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Doacao_EstabelecimentoId",
                table: "Tb_Doacao",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_DoacaoItem_ItemId",
                table: "Tb_DoacaoItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Estabelecimento_EnderecoId",
                table: "Tb_Estabelecimento",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Ong_EnderecoId",
                table: "Tb_Ong",
                column: "EnderecoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_DoacaoItem");

            migrationBuilder.DropTable(
                name: "Tb_Ong");

            migrationBuilder.DropTable(
                name: "Tb_Doacao");

            migrationBuilder.DropTable(
                name: "Tb_Item");

            migrationBuilder.DropTable(
                name: "Tb_Estabelecimento");

            migrationBuilder.DropTable(
                name: "Tb_Endereco");
        }
    }
}
