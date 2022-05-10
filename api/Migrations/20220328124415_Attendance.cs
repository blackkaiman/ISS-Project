using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YonderfulApi.Migrations
{
    public partial class Attendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Autocancel",
                table: "Events",
                newName: "AutoCancel");

            migrationBuilder.CreateTable(
                name: "Attendace",
                columns: table => new
                {
                    EvenimentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipantId = table.Column<int>(type: "INTEGER", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendace", x => new { x.EvenimentId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_Attendace_Events_EvenimentId",
                        column: x => x.EvenimentId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendace_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendace_ParticipantId",
                table: "Attendace",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendace");

            migrationBuilder.RenameColumn(
                name: "AutoCancel",
                table: "Events",
                newName: "Autocancel");
        }
    }
}
