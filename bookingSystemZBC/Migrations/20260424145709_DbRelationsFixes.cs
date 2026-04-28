using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingSystemZBC.Migrations
{
    /// <inheritdoc />
    public partial class DbRelationsFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivitySessions_Activities_ActivityId",
                table: "ActivitySessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivitySessions_Locations_LocationId",
                table: "ActivitySessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Members_MemberId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitySessions_Activities_ActivityId",
                table: "ActivitySessions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitySessions_Locations_LocationId",
                table: "ActivitySessions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Members_MemberId",
                table: "Bookings",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivitySessions_Activities_ActivityId",
                table: "ActivitySessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivitySessions_Locations_LocationId",
                table: "ActivitySessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Members_MemberId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Locations");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitySessions_Activities_ActivityId",
                table: "ActivitySessions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitySessions_Locations_LocationId",
                table: "ActivitySessions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Members_MemberId",
                table: "Bookings",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
