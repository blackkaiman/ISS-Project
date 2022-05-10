using Microsoft.EntityFrameworkCore.Migrations;

namespace YonderfulApi.Migrations
{
    public partial class RenameAttendanceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Events_EvenimentId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Users_ParticipantId",
                table: "Attendance");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "Attendance",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EvenimentId",
                table: "Attendance",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_ParticipantId",
                table: "Attendance",
                newName: "IX_Attendance_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Events_EventId",
                table: "Attendance",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Users_UserId",
                table: "Attendance",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Events_EventId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Users_UserId",
                table: "Attendance");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Attendance",
                newName: "ParticipantId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Attendance",
                newName: "EvenimentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_UserId",
                table: "Attendance",
                newName: "IX_Attendance_ParticipantId");

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
    }
}
