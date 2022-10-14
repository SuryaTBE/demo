using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo.Migrations
{
    public partial class migr13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTbl_movieTbls_MovieId",
                table: "BookingTbl");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "movieTbls",
                newName: "Date");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "BookingTbl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTbl_movieTbls_MovieId",
                table: "BookingTbl",
                column: "MovieId",
                principalTable: "movieTbls",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTbl_movieTbls_MovieId",
                table: "BookingTbl");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "movieTbls",
                newName: "From");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "BookingTbl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTbl_movieTbls_MovieId",
                table: "BookingTbl",
                column: "MovieId",
                principalTable: "movieTbls",
                principalColumn: "MovieId");
        }
    }
}
