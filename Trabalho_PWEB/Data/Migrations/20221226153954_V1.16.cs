using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V116 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Categoria_CategoriaId",
                table: "Veiculo");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Veiculo",
                newName: "CategoriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Veiculo_CategoriaId",
                table: "Veiculo",
                newName: "IX_Veiculo_CategoriaID");

            migrationBuilder.AddColumn<bool>(
                name: "Ativado",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Categoria_CategoriaID",
                table: "Veiculo",
                column: "CategoriaID",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Categoria_CategoriaID",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Ativado",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CategoriaID",
                table: "Veiculo",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Veiculo_CategoriaID",
                table: "Veiculo",
                newName: "IX_Veiculo_CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Categoria_CategoriaId",
                table: "Veiculo",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
