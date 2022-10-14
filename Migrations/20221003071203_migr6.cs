using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo.Migrations
{
    public partial class migr6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderMasterTbls",
                columns: table => new
                {
                    OrderMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "501, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardNo = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMasterTbls", x => x.OrderMasterId);
                    table.ForeignKey(
                        name: "FK_OrderMasterTbls_userTbls_UserId",
                        column: x => x.UserId,
                        principalTable: "userTbls",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "601, 1"),
                    NoOfTickets = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    OrderMasterTblOrderMasterId = table.Column<int>(type: "int", nullable: true),
                    BookingTblBookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_BookingTbl_BookingTblBookingId",
                        column: x => x.BookingTblBookingId,
                        principalTable: "BookingTbl",
                        principalColumn: "BookingId");
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderMasterTbls_OrderMasterTblOrderMasterId",
                        column: x => x.OrderMasterTblOrderMasterId,
                        principalTable: "OrderMasterTbls",
                        principalColumn: "OrderMasterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BookingTblBookingId",
                table: "OrderDetails",
                column: "BookingTblBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderMasterTblOrderMasterId",
                table: "OrderDetails",
                column: "OrderMasterTblOrderMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasterTbls_UserId",
                table: "OrderMasterTbls",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderMasterTbls");
        }
    }
}
