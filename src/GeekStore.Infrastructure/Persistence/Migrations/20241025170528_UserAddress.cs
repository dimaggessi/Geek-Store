using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fa9c612-89b7-4970-abd1-b19abc1ef65c");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec41b536-2e1c-448c-a801-8e12a76f3248");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "identity",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bf58df4-8708-4fd8-ba9f-ba5f6c74c6e5", null, "Admin", "ADMIN" },
                    { "ed854c7c-e69f-482c-989f-83e1e3d3ae70", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                schema: "identity",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                schema: "identity",
                table: "AspNetUsers",
                column: "AddressId",
                principalSchema: "ecommerce",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bf58df4-8708-4fd8-ba9f-ba5f6c74c6e5");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed854c7c-e69f-482c-989f-83e1e3d3ae70");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8fa9c612-89b7-4970-abd1-b19abc1ef65c", null, "Customer", "CUSTOMER" },
                    { "ec41b536-2e1c-448c-a801-8e12a76f3248", null, "Admin", "ADMIN" }
                });
        }
    }
}
