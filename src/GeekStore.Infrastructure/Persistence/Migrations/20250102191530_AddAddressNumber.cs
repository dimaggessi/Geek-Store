using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16a1dc20-56b4-4cf9-9fc0-dcc638ce9027");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90e8f38d-dbc2-4fde-94f4-46a36e1a9482");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                schema: "ecommerce",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Number",
                schema: "ecommerce",
                table: "Addresses");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16a1dc20-56b4-4cf9-9fc0-dcc638ce9027", null, "Customer", "CUSTOMER" },
                    { "90e8f38d-dbc2-4fde-94f4-46a36e1a9482", null, "Admin", "ADMIN" }
                });
        }
    }
}
