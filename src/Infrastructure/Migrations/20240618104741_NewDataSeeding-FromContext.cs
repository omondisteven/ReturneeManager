using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReturneeManager.Infrastructure.Migrations
{
    public partial class NewDataSeedingFromContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BAGERHAT", null, null, "BAGERHAT" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BANDARBAN", null, null, "BANDARBAN" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BARGUNA", null, null, "BARGUNA" }
                });

            migrationBuilder.InsertData(
                table: "Divisions",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BARISAL", null, null, "BARISAL" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CHITAGONG", null, null, "CHITAGONG" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DHAKA", null, null, "DHAKA" }
                });

            migrationBuilder.InsertData(
                table: "FromCountries",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afghanistan 1", null, null, "Afghanistan" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pakistan", null, null, "Pakistan" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iran", null, null, "Iran" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unknown gender", null, null, "Unknown" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female gender", null, null, "Female" },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male gender", null, null, "Male" }
                });

            migrationBuilder.InsertData(
                table: "IdTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Passport", null, null, "Passport" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birth Certificate", null, null, "Birth Certificate" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "National ID", null, null, "National ID" }
                });

            migrationBuilder.InsertData(
                table: "Upazilas",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ABHAYNAGAR ", null, null, "ABHAYNAGAR" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADAMDIGHI", null, null, "ADAMDIGHI" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADARSHA SADAR", null, null, "ADARSHA SADAR" }
                });

            migrationBuilder.InsertData(
                table: "Wards",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BAGHUTIA", null, null, "BAGHUTIA" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CHALISHA", null, null, "CHALISHA" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAYRA", null, null, "PAYRA" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Divisions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FromCountries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IdTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Upazilas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BAGERHAT", null, null, "BAGERHAT" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BANDARBAN", null, null, "BANDARBAN" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BARGUNA", null, null, "BARGUNA" }
                });

            migrationBuilder.InsertData(
                table: "Divisions",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BARISAL", null, null, "BARISAL" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CHITAGONG", null, null, "CHITAGONG" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DHAKA", null, null, "DHAKA" }
                });

            migrationBuilder.InsertData(
                table: "FromCountries",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afghanistan 1", null, null, "Afghanistan" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pakistan", null, null, "Pakistan" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iran", null, null, "Iran" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unknown gender", null, null, "Unknown" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female gender", null, null, "Female" },
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male gender", null, null, "Male" }
                });

            migrationBuilder.InsertData(
                table: "IdTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Passport", null, null, "Passport" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birth Certificate", null, null, "Birth Certificate" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "National ID", null, null, "National ID" }
                });

            migrationBuilder.InsertData(
                table: "Upazilas",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ABHAYNAGAR ", null, null, "ABHAYNAGAR" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADAMDIGHI", null, null, "ADAMDIGHI" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADARSHA SADAR", null, null, "ADARSHA SADAR" }
                });

            migrationBuilder.InsertData(
                table: "Wards",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BAGHUTIA", null, null, "BAGHUTIA" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CHALISHA", null, null, "CHALISHA" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAYRA", null, null, "PAYRA" }
                });
        }
    }
}
