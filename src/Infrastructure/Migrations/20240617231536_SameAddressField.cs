using Microsoft.EntityFrameworkCore.Migrations;

namespace ReturneeManager.Infrastructure.Migrations
{
    public partial class SameAddressField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SameAddress",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SameAddress",
                table: "Persons");
        }
    }
}
