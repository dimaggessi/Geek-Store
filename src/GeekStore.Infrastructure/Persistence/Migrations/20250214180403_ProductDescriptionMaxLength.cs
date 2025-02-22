using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductDescriptionMaxLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ecommerce",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9e8f2c8a-0419-4521-9ab2-5d4f4e8f47cb", null, "Admin", "ADMIN" },
                    { "b27cd934-36b0-4c4f-a282-d8637fd7d217", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e8f2c8a-0419-4521-9ab2-5d4f4e8f47cb");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b27cd934-36b0-4c4f-a282-d8637fd7d217");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ecommerce",
                table: "Products",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

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
    }
}
