using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Site.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Blog_Id",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blog_Id",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Comments");
        }
    }
}
