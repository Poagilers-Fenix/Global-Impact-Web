using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Impact.Migrations
{
    public partial class BD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_Endereco",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<string>(nullable: false),
                    Bairro = table.Column<string>(nullable: false),
                    Cidade = table.Column<string>(nullable: false),
                    UF = table.Column<string>(nullable: false),
                    Logradouro = table.Column<string>(maxLength: 40, nullable: false),
                    Numero = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: true)
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
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Foto = table.Column<string>(nullable: true)
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
                    Nome = table.Column<string>(nullable: false),
                    Cnpj = table.Column<string>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Estabelecimento", x => x.EstabelecimentoId);
                    table.ForeignKey(
                        name: "FK_Tb_Estabelecimento_Tb_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Tb_Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Ong",
                columns: table => new
                {
                    OngId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Ong", x => x.OngId);
                    table.ForeignKey(
                        name: "FK_Tb_Ong_Tb_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Tb_Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Tb_DoacaoAlimento",
                columns: table => new
                {
                    DoacaoId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    DataValidade = table.Column<DateTime>(nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    UnidadeMedida = table.Column<int>(nullable: false),
                    Foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_DoacaoAlimento", x => new { x.DoacaoId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_Tb_DoacaoAlimento_Tb_Doacao_DoacaoId",
                        column: x => x.DoacaoId,
                        principalTable: "Tb_Doacao",
                        principalColumn: "DoacaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_DoacaoAlimento_Tb_Item_ItemId",
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
                name: "IX_Tb_DoacaoAlimento_ItemId",
                table: "Tb_DoacaoAlimento",
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
                name: "Tb_DoacaoAlimento");

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
