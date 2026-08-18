using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "The Weekenders" },
                    { 2, "Northern Lights" },
                    { 3, "Stockholm Beats" }
                });

            migrationBuilder.InsertData(
                table: "Attendees",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Anna Andersson" },
                    { 2, "Erik Johansson" },
                    { 3, "Sara Karlsson" }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Main Stage" },
                    { 2, "Rock Stage" },
                    { 3, "Electronic Stage" }
                });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "Id", "Genre", "LengthMinutes", "PerformanceTime", "StageId" },
                values: new object[,]
                {
                    { 1, "Pop", 60, new DateTime(2026, 8, 22, 18, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Rock", 75, new DateTime(2026, 8, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Electronic", 90, new DateTime(2026, 8, 22, 22, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AttendeeId", "PriceSek", "PurchaseDate", "ValidThrough" },
                values: new object[,]
                {
                    { 1, 1, 750, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 750, new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 1000, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ArtistPerformance",
                columns: new[] { "ArtistsId", "PerformancesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArtistPerformance",
                keyColumns: new[] { "ArtistsId", "PerformancesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ArtistPerformance",
                keyColumns: new[] { "ArtistsId", "PerformancesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ArtistPerformance",
                keyColumns: new[] { "ArtistsId", "PerformancesId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Attendees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
