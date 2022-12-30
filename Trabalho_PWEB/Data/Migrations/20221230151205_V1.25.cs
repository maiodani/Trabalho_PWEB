using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V125 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReservaAceite",
                table: "Reservas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservaAceite",
                table: "Reservas");
        }
    }
}
