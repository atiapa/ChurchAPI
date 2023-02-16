using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurchApi.Migrations
{
    public partial class AddedBankIdToDocumentAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "Documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_BankId",
                table: "Documents",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BankId",
                table: "AspNetUsers",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Banks_BankId",
                table: "AspNetUsers",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Banks_BankId",
                table: "Documents",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Banks_BankId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Banks_BankId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_BankId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BankId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "AspNetUsers");
        }
    }
}
