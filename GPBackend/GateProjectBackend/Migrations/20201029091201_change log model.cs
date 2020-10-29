using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GateProjectBackend.Migrations
{
    public partial class changelogmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountAdmins",
                keyColumns: new[] { "UserId", "AccountId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Logs",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountTypeId", "ContactEmail", "CreatedAt", "Name" },
                values: new object[] { 2, "soma.makai@gmail.com", new DateTime(2020, 10, 29, 9, 12, 1, 377, DateTimeKind.Utc).AddTicks(2475), "TestHome" });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_AccountId",
                table: "Logs",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Accounts_AccountId",
                table: "Logs",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Accounts_AccountId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_AccountId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountTypeId", "ContactEmail", "CreatedAt", "Name" },
                values: new object[] { 1, "asd@gmail.com", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(509), "TestOffice" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountTypeId", "City", "ContactEmail", "Country", "CreatedAt", "CreatedBy", "ModifiedBy", "MoidifiedAt", "Name", "Street", "StreetNo", "Zip" },
                values: new object[,]
                {
                    { 2, 3, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1259), "SYSTEM", null, null, "TestSchool", "Szuglo utca", "53", "1146" },
                    { 3, 1, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1273), "SYSTEM", null, null, "TestOffice2", "Szuglo utca", "53", "1147" },
                    { 4, 2, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1275), "SYSTEM", null, null, "TestHome", "Szuglo utca", "53", "1148" },
                    { 5, 4, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1276), "SYSTEM", null, null, "TestAccomodation", "Szuglo utca", "53", "1149" },
                    { 6, 1, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1278), "SYSTEM", null, null, "TestOffice", "Szuglo utca", "53", "1150" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birth", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsActive", "LastName", "ModifiedBy", "MoidifiedAt", "RfidKey", "RoleId" },
                values: new object[] { 1, new DateTime(1992, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 18, 17, 23, 30, 118, DateTimeKind.Utc).AddTicks(6235), "SYSTEM", "soma.makai@gmail.com", "Soma", true, "Makai", null, null, null, 2 });

            migrationBuilder.InsertData(
                table: "AccountAdmins",
                columns: new[] { "UserId", "AccountId", "CreatedAt", "CreatedBy", "ModifiedBy", "MoidifiedAt" },
                values: new object[] { 1, 1, new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(2520), "SYSTEM", null, null });
        }
    }
}
