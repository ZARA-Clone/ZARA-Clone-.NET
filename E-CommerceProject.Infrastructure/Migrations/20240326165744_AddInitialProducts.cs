using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_CommerceProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "Description", "Discount", "Model", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "test p1", 10m, "M1", "product1", 1000m },
                    { 2, 1, "test p2", 20m, "M2", "product2", 2000m },
                    { 3, 2, "test p3", 30m, "M3", "product3", 3000m },
                    { 4, 3, "test p4", 40m, "M4", "product4", 4000m },
                    { 5, 3, "test p5", 50m, "M5", "product5", 5000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
