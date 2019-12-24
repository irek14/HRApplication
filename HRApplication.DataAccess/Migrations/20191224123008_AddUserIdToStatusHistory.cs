using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.DataAccess.Migrations
{
    public partial class AddUserIdToStatusHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ApplicationStatusHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatusHistory_UserId",
                table: "ApplicationStatusHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStatusHistory_Users",
                table: "ApplicationStatusHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStatusHistory_Users",
                table: "ApplicationStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationStatusHistory_UserId",
                table: "ApplicationStatusHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationStatusHistory");
        }
    }
}
