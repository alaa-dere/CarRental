using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "IsAvailable", "Location", "Make", "Model", "PricePerDay", "Year" },
                values: new object[,]
                {
                    { 1, true, "Nablus", "Toyota", "Corolla", 40.00m, 2024 },
                    { 2, true, "Ramallah", "Kia", "Sportage", 65.00m, 2023 },
                    { 3, true, "Nablus", "Hyundai", "Elantra", 45.00m, 2022 },
                    { 4, false, "Ramallah", "BMW", "X5", 120.00m, 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
