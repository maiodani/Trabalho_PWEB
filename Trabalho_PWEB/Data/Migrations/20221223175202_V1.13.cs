using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Categoria_categoriaId",
                table: "Veiculo");

            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "Veiculo",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Veiculo_categoriaId",
                table: "Veiculo",
                newName: "IX_Veiculo_CategoriaId");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Veiculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Categoria_CategoriaId",
                table: "Veiculo",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Categoria_CategoriaId",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Veiculo");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Veiculo",
                newName: "categoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Veiculo_CategoriaId",
                table: "Veiculo",
                newName: "IX_Veiculo_categoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Categoria_categoriaId",
                table: "Veiculo",
                column: "categoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
