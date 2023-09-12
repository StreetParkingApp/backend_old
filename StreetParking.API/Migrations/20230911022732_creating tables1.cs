using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreetParking.API.Migrations
{
    public partial class creatingtables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Test Descriptionsss");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Test Descriptions");
        }
    }
}
