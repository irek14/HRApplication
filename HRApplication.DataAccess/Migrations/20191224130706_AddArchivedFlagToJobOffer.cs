using Microsoft.EntityFrameworkCore.Migrations;

namespace HRApplication.DataAccess.Migrations
{
    public partial class AddArchivedFlagToJobOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Offers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Offers");
        }
    }
}
