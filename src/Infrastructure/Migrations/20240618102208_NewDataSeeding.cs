using Microsoft.EntityFrameworkCore.Migrations;

namespace ReturneeManager.Infrastructure.Migrations
{
    public partial class NewDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "BAGERHAT");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "BANDARBAN");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "BARGUNA");

            migrationBuilder.UpdateData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "BARISAL");

            migrationBuilder.UpdateData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "CHITAGONG");

            migrationBuilder.UpdateData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "DHAKA");

            migrationBuilder.UpdateData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Afghanistan 1");

            migrationBuilder.UpdateData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Pakistan");

            migrationBuilder.UpdateData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Iran");

            migrationBuilder.UpdateData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Passport");

            migrationBuilder.UpdateData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Birth Certificate");

            migrationBuilder.UpdateData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "National ID");

            migrationBuilder.UpdateData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "ABHAYNAGAR ");

            migrationBuilder.UpdateData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "ADAMDIGHI");

            migrationBuilder.UpdateData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "ADARSHA SADAR");

            migrationBuilder.UpdateData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "BAGHUTIA");

            migrationBuilder.UpdateData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "CHALISHA");

            migrationBuilder.UpdateData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "PAYRA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "District 1");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "District 2");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "District 3");

            migrationBuilder.UpdateData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Division 1");

            migrationBuilder.UpdateData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Division 2");

            migrationBuilder.UpdateData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Division 3");

            migrationBuilder.UpdateData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "FromCountry 1");

            migrationBuilder.UpdateData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "FromCountry 2");

            migrationBuilder.UpdateData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "FromCountry 3");

            migrationBuilder.UpdateData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Description1");

            migrationBuilder.UpdateData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Description2");

            migrationBuilder.UpdateData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Description2");

            migrationBuilder.UpdateData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Upazila 1");

            migrationBuilder.UpdateData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Upazila 2");

            migrationBuilder.UpdateData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Upazila 3");

            migrationBuilder.UpdateData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Ward 1");

            migrationBuilder.UpdateData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Ward 2");

            migrationBuilder.UpdateData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Ward 3");
        }
    }
}
