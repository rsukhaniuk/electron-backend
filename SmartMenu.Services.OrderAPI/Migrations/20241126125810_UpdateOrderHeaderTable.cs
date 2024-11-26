using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMenu.Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderHeaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "OrderHeaders",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "OrderHeaders");
        }
    }
}
