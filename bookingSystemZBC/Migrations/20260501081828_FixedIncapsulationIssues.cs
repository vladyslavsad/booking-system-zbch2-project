using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingSystemZBC.Migrations
{
    /// <inheritdoc />
    public partial class FixedIncapsulationIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddtionalInfoField1",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddtionalInfoField1",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddtionalInfoField1",
                value: "Hatha flow");
        }
    }
}
