using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YonderfulApi.Migrations
{
  public partial class UserDetails : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Categories",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Title = table.Column<string>(type: "TEXT", nullable: true),
            IconId = table.Column<int>(type: "INTEGER", nullable: false),
            DefaultBackgroundId = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Categories", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Pictures",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            FileType = table.Column<string>(type: "TEXT", nullable: true),
            Content = table.Column<byte[]>(type: "BLOB", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Pictures", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true),
            Role = table.Column<int>(type: "INTEGER", nullable: false),
            Email = table.Column<string>(type: "TEXT", nullable: true),
            Password = table.Column<string>(type: "TEXT", nullable: true),
            Position = table.Column<string>(type: "TEXT", nullable: true),
            PhoneNo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Categories");

      migrationBuilder.DropTable(
          name: "Pictures");

      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
