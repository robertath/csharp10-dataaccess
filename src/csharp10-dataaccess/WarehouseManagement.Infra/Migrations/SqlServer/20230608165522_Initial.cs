using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagement.Infra.Migrations.SqlServer
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Address = table.Column<string>(type: "varchar(200)", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    Country = table.Column<string>(type: "varchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProvider",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    FreightCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_ShippingProvider_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "ShippingProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemWarehouse",
                columns: table => new
                {
                    ItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehousesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemWarehouse", x => new { x.ItemsId, x.WarehousesId });
                    table.ForeignKey(
                        name: "FK_ItemWarehouse_Item_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemWarehouse_Warehouse_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Customer_Id",
                table: "Customer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IDX_Customer_Name",
                table: "Customer",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IDX_Item_Id",
                table: "Item",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_WarehousesId",
                table: "ItemWarehouse",
                column: "WarehousesId");

            migrationBuilder.CreateIndex(
                name: "IDX_LineItem_ItemId",
                table: "LineItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IDX_LineItem_OrderId",
                table: "LineItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IDX_Order_Id",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShippingProviderId",
                table: "Order",
                column: "ShippingProviderId");

            migrationBuilder.CreateIndex(
                name: "IDX_ShippingProvider_Id",
                table: "ShippingProvider",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IDX_Warehouse_Id",
                table: "Warehouse",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemWarehouse");

            migrationBuilder.DropTable(
                name: "LineItem");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "ShippingProvider");
        }
    }
}
