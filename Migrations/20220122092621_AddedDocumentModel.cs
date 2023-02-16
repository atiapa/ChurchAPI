using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurchApi.Migrations
{
    public partial class AddedDocumentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Documents",
                newName: "RootPath");

            migrationBuilder.AddColumn<string>(
                name: "FileLabel",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReferenceId",
                table: "Documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileLabel",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "RootPath",
                table: "Documents",
                newName: "Name");
        }
    }
}
