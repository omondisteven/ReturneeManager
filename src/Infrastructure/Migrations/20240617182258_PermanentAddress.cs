using Microsoft.EntityFrameworkCore.Migrations;

namespace ReturneeManager.Infrastructure.Migrations
{
    public partial class PermanentAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId2",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DivisionId2",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HouseVillage2",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode2",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress2",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpazilaId2",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardId2",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId2",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DivisionId2",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "HouseVillage2",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PostCode2",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "StreetAddress2",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UpazilaId2",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "WardId2",
                table: "Persons");
        }
    }
}
