using Microsoft.EntityFrameworkCore.Migrations;

namespace CoordinatorTaskProject.Migrations
{
    public partial class addedupdateNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdateNumber",
                table: "Updates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateNumber",
                table: "Updates");
        }
    }
}
