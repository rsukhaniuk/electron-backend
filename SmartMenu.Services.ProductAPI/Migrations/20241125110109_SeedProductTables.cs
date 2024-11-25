using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMenu.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStores",
                columns: table => new
                {
                    ProductStoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStores", x => x.ProductStoreId);
                    table.ForeignKey(
                        name: "FK_ProductStores_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStores_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Kitchen Appliances" },
                    { 2, "Cleaning Appliances" },
                    { 3, "Personal Care Appliances" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "Address", "Hours", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "9am - 9pm", "Downtown Appliance Store", "555-1234" },
                    { 2, "456 Elm Street", "10am - 8pm", "Suburban Appliance Center", "555-5678" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Compact and powerful microwave oven with multiple cooking modes and a digital timer.", null, "https://placehold.co/600x400", "Microwave Oven", 120.0 },
                    { 2, 1, "Energy-efficient refrigerator with spacious compartments and temperature control.", null, "https://placehold.co/600x400", "Refrigerator", 450.0 },
                    { 3, 2, "High-suction vacuum cleaner with HEPA filters for deep cleaning.", null, "https://placehold.co/600x400", "Vacuum Cleaner", 180.0 },
                    { 4, 2, "Front-load washing machine with multiple wash settings and energy-saving mode.", null, "https://placehold.co/600x400", "Washing Machine", 300.0 },
                    { 5, 3, "Ergonomic electric shaver with a rechargeable battery for a smooth shave.", null, "https://placehold.co/600x400", "Electric Shaver", 50.0 },
                    { 6, 3, "Compact hair dryer with multiple heat and speed settings.", null, "https://placehold.co/600x400", "Hair Dryer", 40.0 }
                });

            migrationBuilder.InsertData(
                table: "ProductStores",
                columns: new[] { "ProductStoreId", "IsAvailable", "ProductId", "StoreId" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 2, 1 },
                    { 3, true, 3, 2 },
                    { 4, false, 4, 2 },
                    { 5, true, 5, 1 },
                    { 6, true, 6, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStores_ProductId",
                table: "ProductStores",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStores_StoreId",
                table: "ProductStores",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStores");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
