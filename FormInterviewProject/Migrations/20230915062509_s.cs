using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormInterviewProject.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Persons",
                newName: "Gender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Persons",
                newName: "MyProperty");
        }
    }
}
