using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Data.Migrations
{
    public partial class UpdatedModelClassAndControllers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Workout",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Workout");
        }
    }
}
