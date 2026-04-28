using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bookingSystemZBC.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "ActivityType", "AdditionalInfoField1", "AddtionalInfoField1", "Level", "MaxParticipants", "Name", "Price", "Type" },
                values: new object[] { 1, "Yoga", "Hatha flow", "Hatha flow", "Beginner", 20, "Morning Yoga", 75m, 3 });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "ActivityType", "AdditionalInfoField1", "Level", "MaxParticipants", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 2, "Swimming", "Lane training", "Intermediate", 16, "Evening Swim", 95m, 1 },
                    { 3, "Climbing", "Instructor included", "Beginner", 12, "Intro Climbing", 125m, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Capacity",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Capacity",
                value: 30);

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Capacity", "Description", "IsAvailable", "Name" },
                values: new object[] { 3, 24, "Indoor climbing wall with instructor area", true, "Climbing Wall" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Age", "Email", "Name" },
                values: new object[] { 3, 22, "clara@example.com", "Clara Madsen" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "MemberId", "Password", "Surname", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "System", null, "Admin123!", "Administrator", "admin" },
                    { 2, "alice@example.com", "Alice", 1, "Password123!", "Jensen", "alice" },
                    { 3, "bob@example.com", "Bob", 2, "Password123!", "Nielsen", "bob" }
                });

            migrationBuilder.InsertData(
                table: "ActivitySessions",
                columns: new[] { "Id", "ActivityId", "EndTimeUtc", "LocationId", "Notes", "StartTimeUtc" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 5, 4, 17, 0, 0, 0, DateTimeKind.Utc), 1, "Bring your own mat if you prefer.", new DateTime(2026, 5, 4, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2026, 5, 5, 18, 30, 0, 0, DateTimeKind.Utc), 2, "Meet at the pool entrance 10 minutes before start.", new DateTime(2026, 5, 5, 17, 30, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2026, 5, 6, 17, 0, 0, 0, DateTimeKind.Utc), 3, "Harnesses are provided.", new DateTime(2026, 5, 6, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 1, new DateTime(2026, 5, 7, 8, 30, 0, 0, DateTimeKind.Utc), 1, "Quiet morning session.", new DateTime(2026, 5, 7, 7, 30, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "MemberId", "Password", "Surname", "UserName" },
                values: new object[] { 4, "clara@example.com", "Clara", 3, "Password123!", "Madsen", "clara" });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "ActivitySessionId", "CreatedAtUtc", "MemberId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 28, 9, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 2, new DateTime(2026, 4, 28, 9, 15, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, 3, new DateTime(2026, 4, 28, 9, 30, 0, 0, DateTimeKind.Utc), 3 },
                    { 4, 4, new DateTime(2026, 4, 28, 9, 45, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ActivitySessions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActivitySessions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActivitySessions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ActivitySessions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Capacity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Capacity",
                value: 0);
        }
    }
}
