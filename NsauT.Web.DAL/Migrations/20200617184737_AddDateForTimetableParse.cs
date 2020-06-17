using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NsauT.Web.DAL.Migrations
{
    public partial class AddDateForTimetableParse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Timetables",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NotChanged",
                table: "Timetables",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Timetables",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "NotChanged",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Timetables");
        }
    }
}
