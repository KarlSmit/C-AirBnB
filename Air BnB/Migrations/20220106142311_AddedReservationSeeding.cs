using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Air_BnB.Migrations
{
    public partial class AddedReservationSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CustomerId", "EndDate", "RoomId", "StartDate" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2022, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2022, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2022, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(2022, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2022, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, new DateTime(2022, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2022, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 3, new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2022, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, new DateTime(2022, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2022, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
