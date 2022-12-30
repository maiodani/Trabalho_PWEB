using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_PWEB.Data.Migrations
{
    public partial class V126 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoCarro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idReserva = table.Column<int>(type: "int", nullable: false),
                    reservaId = table.Column<int>(type: "int", nullable: false),
                    nKm = table.Column<float>(type: "real", nullable: false),
                    DadosCarro = table.Column<bool>(type: "bit", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idReservante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    funcionarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCarro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadoCarro_AspNetUsers_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadoCarro_Reservas_reservaId",
                        column: x => x.reservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadoCarro_funcionarioId",
                table: "EstadoCarro",
                column: "funcionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoCarro_reservaId",
                table: "EstadoCarro",
                column: "reservaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoCarro");
        }
    }
}
