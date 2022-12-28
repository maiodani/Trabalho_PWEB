using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V124 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_AspNetUsers_ReservanteId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_ReservanteId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "ReservanteId",
                table: "Reservas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservanteId",
                table: "Reservas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ReservanteId",
                table: "Reservas",
                column: "ReservanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_AspNetUsers_ReservanteId",
                table: "Reservas",
                column: "ReservanteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
