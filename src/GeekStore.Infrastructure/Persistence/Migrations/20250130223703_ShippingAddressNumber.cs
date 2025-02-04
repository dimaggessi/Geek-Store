using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShippingAddressNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "162a8916-c5a8-419f-bb52-84dfb009b395");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2481be9b-9e0f-41e6-8a99-dc94c62a7332");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Number",
                schema: "ecommerce",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ec0a9e4-2b7a-48b8-bd99-9dff0e977cbf", null, "Customer", "CUSTOMER" },
                    { "eaefcad0-e9c1-47a2-bdf1-ed5fdf747d89", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ec0a9e4-2b7a-48b8-bd99-9dff0e977cbf");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eaefcad0-e9c1-47a2-bdf1-ed5fdf747d89");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Number",
                schema: "ecommerce",
                table: "Orders");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "162a8916-c5a8-419f-bb52-84dfb009b395", null, "Customer", "CUSTOMER" },
                    { "2481be9b-9e0f-41e6-8a99-dc94c62a7332", null, "Admin", "ADMIN" }
                });
        }
    }
}
