using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.DataAccess.Migrations
{
    public partial class SalaryFromToColumnAddedToOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryFrom",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryTo",
                table: "Offers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryFrom",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "SalaryTo",
                table: "Offers");
        }
    }
}
