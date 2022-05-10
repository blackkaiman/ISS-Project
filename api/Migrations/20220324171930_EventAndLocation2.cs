using Microsoft.EntityFrameworkCore.Migrations;

namespace YonderfulApi.Migrations
{
    public partial class EventAndLocation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Autocancel",
                table: "Events",
                newName: "AutoCancel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AutoCancel",
                table: "Events",
                newName: "Autocancel");
        }
    }
}
