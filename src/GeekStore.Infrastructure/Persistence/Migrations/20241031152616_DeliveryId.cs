using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e80b2ff-c540-4aa2-9301-9213342df3a5");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c06727e2-b65f-4187-9b30-fa444b4689e6");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethod_Id",
                schema: "ecommerce",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DeliveryMethod_Id",
                schema: "ecommerce",
                table: "Orders");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8e80b2ff-c540-4aa2-9301-9213342df3a5", null, "Admin", "ADMIN" },
                    { "c06727e2-b65f-4187-9b30-fa444b4689e6", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
