using Microsoft.EntityFrameworkCore.Migrations;

namespace AppBayBack.Migrations
{
    public partial class UpdateUsersTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGranted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGranted",
                table: "AspNetUsers");
        }
    }
}
