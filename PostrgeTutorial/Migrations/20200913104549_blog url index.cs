using Microsoft.EntityFrameworkCore.Migrations;

namespace PostrgeTutorial.Migrations
{
    public partial class blogurlindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Url",
                table: "Blogs",
                column: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blogs_Url",
                table: "Blogs");
        }
    }
}
