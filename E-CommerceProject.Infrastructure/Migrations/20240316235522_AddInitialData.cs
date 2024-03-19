using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_CommerceProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCarts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCarts", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_UserCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WishLists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliverdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    UserAddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_UserAddresses_UserAddressId",
                        column: x => x.UserAddressId,
                        principalTable: "UserAddresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.UserId, x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lenovo" },
                    { 2, "Dell" },
                    { 3, "HP" },
                    { 4, "Apple" },
                    { 5, "Asus" },
                    { 6, "Acer" },
                    { 7, "Microsoft" },
                    { 8, "MSI" },
                    { 9, "Alienware" },
                    { 10, "Samsung" },
                    { 11, "Huawei" },
                    { 12, "Fujitsu" },
                    { 13, "Google" },
                    { 14, "Razer" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "Description", "Discount", "Model", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 4, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 50m, "MacBook Air", "Apple MacBook Air", 40000m },
                    { 2, 4, "The Apple MacBook Pro is a high-performance laptop loved by professionals.", 20m, "MacBook Pro", "Apple MacBook Pro", 80000m },
                    { 3, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 0m, "XPS 13", "Dell XPS 13", 40000m },
                    { 4, 2, "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.", 13m, "Inspiron 15", "Dell Inspiron 15", 35000m },
                    { 5, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 15m, "Spectre x360", "HP Spectre x360", 25000m },
                    { 6, 3, "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.", 60m, "Pavilion 14", "HP Pavilion 14", 15000m },
                    { 7, 4, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 10m, "MacBook Air", "Apple MacBook Air", 28000m },
                    { 8, 4, "The Apple MacBook Pro is a high-performance laptop loved by professionals.", 12m, "MacBook Pro", "Apple MacBook Pro", 30000m },
                    { 9, 4, "The Apple iMac is a sleek and powerful all-in-one desktop computer.", 0m, "iMac", "Apple iMac", 16000m },
                    { 10, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 90m, "XPS 13", "Dell XPS 13", 14000m },
                    { 11, 2, "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.", 18m, "Inspiron 15", "Dell Inspiron 15", 30000m },
                    { 12, 2, "The Dell G5 Gaming Desktop is a powerful gaming machine with immersive graphics.", 20m, "G5 Gaming Desktop", "Dell G5 Gaming Desktop", 38000m },
                    { 13, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 19m, "Spectre x360", "HP Spectre x360", 26000m },
                    { 14, 3, "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.", 0m, "Pavilion 14", "HP Pavilion 14", 6000m },
                    { 15, 3, "The HP EliteBook 840 is a business laptop with top-notch security features.", 80m, "EliteBook 840", "HP EliteBook 840", 50000m },
                    { 16, 4, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 15m, "MacBook Air", "Apple MacBook Air", 18000m },
                    { 17, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 5m, "XPS 13", "Dell XPS 13", 13000m },
                    { 18, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 10m, "Spectre x360", "HP Spectre x360", 12000m },
                    { 19, 1, "The Lenovo ThinkCentre M720 is a compact and reliable desktop computer for business use.", 6m, "ThinkCentre M720", "Lenovo ThinkCentre M720", 15000m },
                    { 20, 5, "The ASUS ROG Strix G15 is a powerful gaming desktop with RGB lighting and high-performance components.", 60m, "ROG Strix G15", "ASUS ROG Strix G15", 80000m },
                    { 21, 6, "The Acer Aspire TC is a budget-friendly desktop computer with decent performance.", 15m, "Aspire TC", "Acer Aspire TC", 18000m },
                    { 22, 2, "The Dell Inspiron 27 is an all-in-one desktop computer with a large display and powerful performance.", 10m, "Inspiron 27", "Dell Inspiron 27", 22000m },
                    { 23, 5, "The ASUS ZenBook Pro is a premium laptop with a stunning 4K display and high-performance components.", 15m, "ZenBook Pro", "ASUS ZenBook Pro", 28000m },
                    { 24, 3, "The HP Pavilion Gaming Desktop is a gaming powerhouse with advanced graphics and smooth gameplay.", 80m, "Pavilion Gaming Desktop", "HP Pavilion Gaming Desktop", 15000m },
                    { 25, 1, "The Lenovo Legion Y540 is a gaming laptop with powerful hardware and immersive gaming experience.", 12m, "Legion Y540", "Lenovo Legion Y540", 20000m },
                    { 26, 4, "The Apple iMac is a sleek all-in-one desktop computer with a stunning Retina display and powerful performance.", 20m, "iMac", "Apple iMac", 24000m },
                    { 27, 2, "The Dell G5 is a gaming laptop with high-performance hardware and immersive gaming features.", 10m, "G5 Gaming Laptop", "Dell G5 Gaming Laptop", 18000m },
                    { 28, 3, "The HP Envy 15 is a premium laptop with a sleek design and powerful performance for multimedia and productivity tasks.", 15m, "Envy 15", "HP Envy 15", 16000m },
                    { 29, 1, "The Lenovo IdeaCentre 5 is a compact and versatile desktop computer suitable for home and office use.", 50m, "IdeaCentre 5", "Lenovo IdeaCentre 5", 8990m },
                    { 30, 5, "The ASUS VivoBook S15 is a stylish and lightweight laptop with a vibrant display and long battery life.", 0m, "VivoBook S15", "ASUS VivoBook S15", 9990m },
                    { 31, 10, "The Samsung Galaxy Book Pro is a thin and lightweight laptop with a stunning AMOLED display and powerful performance.", 10m, "Galaxy Book Pro", "Samsung Galaxy Book Pro", 14990m },
                    { 32, 2, "The Dell Alienware Aurora R10 is a high-performance gaming desktop with powerful hardware and customizable lighting.", 20m, "Alienware Aurora R10", "Dell Alienware Aurora R10", 28000m },
                    { 33, 3, "The HP Omen 15 is a gaming laptop with a sleek design, high-refresh-rate display, and powerful performance for gaming enthusiasts.", 15m, "Omen 15", "HP Omen 15", 17999m },
                    { 34, 4, "The Apple MacBook Air is a lightweight and portable laptop with a stunning Retina display and all-day battery life.", 10m, "MacBook Air", "Apple MacBook Air", 12990m },
                    { 35, 14, "The Razer Blade 15 is a premium gaming laptop with a sleek design, high-refresh-rate display, and powerful performance.", 15m, "Blade 15", "Razer Blade 15", 23990m },
                    { 36, 1, "The Lenovo ThinkPad X1 Carbon is a premium business laptop with a durable build, long battery life, and top-notch performance.", 20m, "ThinkPad X1 Carbon", "Lenovo ThinkPad X1 Carbon", 18990m },
                    { 37, 5, "The ASUS ROG Zephyrus G14 is a powerful gaming laptop with an ultra-portable design and impressive performance.", 5m, "ROG Zephyrus G14", "ASUS ROG Zephyrus G14", 17000m },
                    { 38, 8, "The MSI GS66 Stealth is a high-performance gaming laptop with a sleek design and powerful components.", 13m, "GS66 Stealth", "MSI GS66 Stealth", 23999m },
                    { 39, 8, "The MSI Prestige 14 is a stylish and powerful laptop designed for creative professionals.", 15m, "Prestige 14", "MSI Prestige 14", 15990m },
                    { 40, 7, "The Microsoft Surface Laptop 4 is a sleek and versatile laptop with a premium design and excellent performance.", 15m, "Surface Laptop 4", "Microsoft Surface Laptop 4", 23000m },
                    { 41, 7, "The Microsoft Surface Pro 7 is a powerful 2-in-1 tablet-laptop hybrid with a detachable keyboard and versatile functionality.", 10m, "Surface Pro 7", "Microsoft Surface Pro 7", 20000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserAddressId",
                table: "Orders",
                column: "UserAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCarts_ProductId",
                table: "UserCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserCarts");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
