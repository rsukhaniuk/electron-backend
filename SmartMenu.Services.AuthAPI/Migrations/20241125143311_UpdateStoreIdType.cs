using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMenu.Services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStoreIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d55eb96f-9d8f-441b-8342-175a53d1de5a", "f375f31a-3536-403b-a6eb-a8a272daa4e5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d55eb96f-9d8f-441b-8342-175a53d1de5a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f375f31a-3536-403b-a6eb-a8a272daa4e5");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4fe07ae-53fc-4f80-8d93-3746ccac9f08", null, "ADMIN", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StoreId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8556aac1-aa4d-485e-926d-bb33d9b50519", 0, "713b7a98-ca08-48cc-b6cd-fb8ed4e5fdd7", "admin@system.com", true, false, null, "System Admin", "ADMIN@SYSTEM.COM", "ADMIN@SYSTEM.COM", "AQAAAAIAAYagAAAAEBba/m+EBRWj9o7n2dcl+/l5IeXwI4drH/kT6SuksBWeMxj6BbHMa6rvp4kb29NKhQ==", null, false, "8ba9bff5-78bf-480c-a33b-9f1b325dcc74", null, false, "admin@system.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e4fe07ae-53fc-4f80-8d93-3746ccac9f08", "8556aac1-aa4d-485e-926d-bb33d9b50519" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e4fe07ae-53fc-4f80-8d93-3746ccac9f08", "8556aac1-aa4d-485e-926d-bb33d9b50519" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4fe07ae-53fc-4f80-8d93-3746ccac9f08");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8556aac1-aa4d-485e-926d-bb33d9b50519");

            migrationBuilder.AlterColumn<string>(
                name: "StoreId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d55eb96f-9d8f-441b-8342-175a53d1de5a", null, "ADMIN", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StoreId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f375f31a-3536-403b-a6eb-a8a272daa4e5", 0, "b28a3c5c-0c95-4029-9717-4cdddedbb6ab", "admin@system.com", true, false, null, "System Admin", "ADMIN@SYSTEM.COM", "ADMIN@SYSTEM.COM", "AQAAAAIAAYagAAAAEMybz/lSc3qht88z8Umee6AwEAlQRNqw9WJUIUQILODKSlSQ8JaVHQhNv0mqcOQy6g==", null, false, "e6c973a1-0e29-472b-bbaf-33bcc842cb37", null, false, "admin@system.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d55eb96f-9d8f-441b-8342-175a53d1de5a", "f375f31a-3536-403b-a6eb-a8a272daa4e5" });
        }
    }
}
