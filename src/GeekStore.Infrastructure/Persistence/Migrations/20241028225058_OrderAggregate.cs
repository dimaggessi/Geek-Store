using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "ecommerce",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShippingAddress_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_Neighborhood = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShippingAddress_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DeliveryMethod_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliveryMethod_Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryMethod_DeliveryTimeInDays = table.Column<int>(type: "int", nullable: false),
                    DeliveryMethod_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DeliveryMethod_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryMethod_Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentSummary_CardLast4 = table.Column<int>(type: "int", nullable: false),
                    PaymentSummary_Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentSummary_ExpMonth = table.Column<int>(type: "int", nullable: false),
                    PaymentSummary_ExpYear = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "ecommerce",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemOrdered_ProductId = table.Column<int>(type: "int", nullable: false),
                    ItemOrdered_ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemOrdered_Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ecommerce",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8e80b2ff-c540-4aa2-9301-9213342df3a5", null, "Admin", "ADMIN" },
                    { "c06727e2-b65f-4187-9b30-fa444b4689e6", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "ecommerce",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "ecommerce");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "ecommerce");

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

            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bf58df4-8708-4fd8-ba9f-ba5f6c74c6e5", null, "Admin", "ADMIN" },
                    { "ed854c7c-e69f-482c-989f-83e1e3d3ae70", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
