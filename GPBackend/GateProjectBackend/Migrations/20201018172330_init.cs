using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GateProjectBackend.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MoidifiedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    StreetNo = table.Column<string>(nullable: true),
                    AccountTypeId = table.Column<int>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MoidifiedAt = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Birth = table.Column<DateTime>(nullable: false),
                    RfidKey = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MoidifiedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    GateTypeId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<string>(nullable: true),
                    CharacteristicId = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gates_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gates_GateTypes_GateTypeId",
                        column: x => x.GateTypeId,
                        principalTable: "GateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountAdmins",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MoidifiedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAdmins", x => new { x.UserId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountAdmins_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountAdmins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MoidifiedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUsers", x => new { x.UserId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountUsers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MoidifiedAt = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    EventTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGates",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GateId = table.Column<int>(nullable: false),
                    AccessRight = table.Column<bool>(nullable: false),
                    AdminRight = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MoidifiedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGates", x => new { x.UserId, x.GateId });
                    table.ForeignKey(
                        name: "FK_UserGates_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Office" },
                    { 2, "Home" },
                    { 3, "School" },
                    { 4, "Accomodation" }
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Error" },
                    { 3, "Info" }
                });

            migrationBuilder.InsertData(
                table: "GateTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Entrance" },
                    { 2, "Garage" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountTypeId", "City", "ContactEmail", "Country", "CreatedAt", "CreatedBy", "ModifiedBy", "MoidifiedAt", "Name", "Street", "StreetNo", "Zip" },
                values: new object[,]
                {
                    { 1, 1, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(509), "SYSTEM", null, null, "TestOffice", "Szuglo utca", "53", "1145" },
                    { 3, 1, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1273), "SYSTEM", null, null, "TestOffice2", "Szuglo utca", "53", "1147" },
                    { 6, 1, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1278), "SYSTEM", null, null, "TestOffice", "Szuglo utca", "53", "1150" },
                    { 4, 2, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1275), "SYSTEM", null, null, "TestHome", "Szuglo utca", "53", "1148" },
                    { 2, 3, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1259), "SYSTEM", null, null, "TestSchool", "Szuglo utca", "53", "1146" },
                    { 5, 4, "Budapest", "asd@gmail.com", "Hungary", new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1276), "SYSTEM", null, null, "TestAccomodation", "Szuglo utca", "53", "1149" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birth", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsActive", "LastName", "ModifiedBy", "MoidifiedAt", "RfidKey", "RoleId" },
                values: new object[] { 1, new DateTime(1992, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 18, 17, 23, 30, 118, DateTimeKind.Utc).AddTicks(6235), "SYSTEM", "soma.makai@gmail.com", "Soma", true, "Makai", null, null, null, 2 });

            migrationBuilder.InsertData(
                table: "AccountAdmins",
                columns: new[] { "UserId", "AccountId", "CreatedAt", "CreatedBy", "ModifiedBy", "MoidifiedAt" },
                values: new object[] { 1, 1, new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(2520), "SYSTEM", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAdmins_AccountId",
                table: "AccountAdmins",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUsers_AccountId",
                table: "AccountUsers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Gates_AccountId",
                table: "Gates",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Gates_GateTypeId",
                table: "Gates",
                column: "GateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_EventTypeId",
                table: "Logs",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGates_GateId",
                table: "UserGates",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAdmins");

            migrationBuilder.DropTable(
                name: "AccountUsers");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "UserGates");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Gates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "GateTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "AccountTypes");
        }
    }
}
