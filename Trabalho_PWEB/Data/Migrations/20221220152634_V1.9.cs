using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstadoSubcricao",
                table: "Empresa",
                newName: "EstadoSubscricao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstadoSubscricao",
                table: "Empresa",
                newName: "EstadoSubcricao");
        }
    }
}
