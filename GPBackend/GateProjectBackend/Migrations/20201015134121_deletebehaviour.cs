using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GateProjectBackend.Migrations
{
    public partial class deletebehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountAdmins",
                keyColumns: new[] { "UserId", "AccountId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(7116));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(7896));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 15, 13, 41, 21, 427, DateTimeKind.Utc).AddTicks(2858));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountAdmins",
                keyColumns: new[] { "UserId", "AccountId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(8025));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(5913));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(6667));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(6751));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(6753));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 12, 16, 54, 0, 645, DateTimeKind.Utc).AddTicks(1518));
        }
    }
}
