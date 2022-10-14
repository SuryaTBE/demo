using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo.Migrations
{
    public partial class migr5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingTbl",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "401, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    NoOfTickets = table.Column<int>(type: "int", nullable: false),
                    AmountTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTbl", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_BookingTbl_movieTbls_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movieTbls",
                        principalColumn: "MovieId");
                    table.ForeignKey(
                        name: "FK_BookingTbl_userTbls_UserId",
                        column: x => x.UserId,
                        principalTable: "userTbls",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTbl_MovieId",
                table: "BookingTbl",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTbl_UserId",
                table: "BookingTbl",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTbl");
        }
    }
}
