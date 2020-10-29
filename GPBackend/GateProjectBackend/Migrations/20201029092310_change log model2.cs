using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GateProjectBackend.Migrations
{
    public partial class changelogmodel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GateId",
                table: "Logs",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 29, 9, 23, 10, 162, DateTimeKind.Utc).AddTicks(8664));

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Enter" });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_GateId",
                table: "Logs",
                column: "GateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Gates_GateId",
                table: "Logs",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Gates_GateId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_GateId",
                table: "Logs");

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "GateId",
                table: "Logs");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 10, 29, 9, 12, 1, 377, DateTimeKind.Utc).AddTicks(2475));
        }
    }
}
