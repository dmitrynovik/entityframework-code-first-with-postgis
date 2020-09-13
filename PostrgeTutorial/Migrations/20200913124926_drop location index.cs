using Microsoft.EntityFrameworkCore.Migrations;

namespace PostrgeTutorial.Migrations
{
    public partial class droplocationindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_City_Location",
                table: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_City_Location",
                table: "City",
                column: "Location");
        }
    }
}
