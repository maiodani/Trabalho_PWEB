using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V129 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Acabou",
                table: "Reservas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acabou",
                table: "Reservas");
        }
    }
}
