using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.DataAccess.Migrations
{
    public partial class AddPKAndIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Offers_EndDate",
                table: "Offers",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationStatusHistoryId",
                table: "Applications",
                column: "ApplicationStatusHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CreateOn",
                table: "Applications",
                column: "CreateOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offers_EndDate",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationStatusHistoryId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_CreateOn",
                table: "Applications");
        }
    }
}
