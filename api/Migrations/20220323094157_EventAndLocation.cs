using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YonderfulApi.Migrations
{
    public partial class EventAndLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pictures");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Pictures",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Province = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    HostId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    StartingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MinimumParticipants = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumParticipants = table.Column<int>(type: "INTEGER", nullable: false),
                    Autocancel = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoJoin = table.Column<bool>(type: "INTEGER", nullable: false),
                    JoinDeadline = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fee = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    EventLocationId = table.Column<int>(type: "INTEGER", nullable: true),
                    ContactEmail = table.Column<string>(type: "TEXT", nullable: true),
                    ContactPhone = table.Column<string>(type: "TEXT", nullable: true),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    BackgroundId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Location_EventLocationId",
                        column: x => x.EventLocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventLocationId",
                table: "Events",
                column: "EventLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "Pictures",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Pictures",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pictures",
                type: "TEXT",
                nullable: true);
        }
    }
}
