using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ócupado",
                table: "Veiculo",
                newName: "Ocupado");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Veiculo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Veiculo");

            migrationBuilder.RenameColumn(
                name: "Ocupado",
                table: "Veiculo",
                newName: "Ócupado");
        }
    }
}
