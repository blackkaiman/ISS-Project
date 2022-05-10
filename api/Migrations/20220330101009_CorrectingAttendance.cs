using Microsoft.EntityFrameworkCore.Migrations;

namespace YonderfulApi.Migrations
{
    public partial class CorrectingAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendace_Events_EvenimentId",
                table: "Attendace");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendace_Users_ParticipantId",
                table: "Attendace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendace",
                table: "Attendace");

            migrationBuilder.RenameTable(
                name: "Attendace",
                newName: "Attendance");

            migrationBuilder.RenameIndex(
                name: "IX_Attendace_ParticipantId",
                table: "Attendance",
                newName: "IX_Attendance_ParticipantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                columns: new[] { "EvenimentId", "ParticipantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Events_EvenimentId",
                table: "Attendance",
                column: "EvenimentId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Users_ParticipantId",
                table: "Attendance",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Events_EvenimentId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Users_ParticipantId",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "Attendace");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_ParticipantId",
                table: "Attendace",
                newName: "IX_Attendace_ParticipantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendace",
                table: "Attendace",
                columns: new[] { "EvenimentId", "ParticipantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendace_Events_EvenimentId",
                table: "Attendace",
                column: "EvenimentId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendace_Users_ParticipantId",
                table: "Attendace",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
