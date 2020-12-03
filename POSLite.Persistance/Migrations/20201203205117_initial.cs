using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POSLite.Persistance.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PIN = table.Column<string>(nullable: true),
                    Logo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    CurrentBalance = table.Column<float>(nullable: false),
                    DateOfLastDeposit = table.Column<DateTime>(nullable: false),
                    AmountOfLastDeposit = table.Column<float>(nullable: false),
                    OtherDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    PaymentMethodCode = table.Column<string>(nullable: true),
                    PaymentMethodName = table.Column<string>(nullable: true),
                    PaymentMethodDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurements",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    UOMCode = table.Column<string>(nullable: true),
                    UOMDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalesOutlets",
                columns: table => new
                {
                    SalesOutletId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhysicalAddress = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOutlets", x => x.SalesOutletId);
                    table.ForeignKey(
                        name: "FK_SalesOutlets_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    BarCode = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UOMCodeId = table.Column<Guid>(nullable: false),
                    BrandId = table.Column<Guid>(nullable: false),
                    CostPrice = table.Column<float>(nullable: false),
                    RetailPrice = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    VAT = table.Column<int>(nullable: false),
                    ReorderLevel = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    OtherDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_ItemCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_UnitOfMeasurements_UOMCodeId",
                        column: x => x.UOMCodeId,
                        principalTable: "UnitOfMeasurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CostCenterId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<long>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staff_SalesOutlets_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "SalesOutlets",
                        principalColumn: "SalesOutletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustment",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    CostCenterId = table.Column<Guid>(nullable: false),
                    AdjQty = table.Column<int>(nullable: false),
                    AdjustmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustment_SalesOutlets_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "SalesOutlets",
                        principalColumn: "SalesOutletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustment_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemInventories",
                columns: table => new
                {
                    InventoryId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    InventoryDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<float>(nullable: false),
                    QuantityInStock = table.Column<float>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<Guid>(nullable: false),
                    OtherDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_ItemInventories_SalesOutlets_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "SalesOutlets",
                        principalColumn: "SalesOutletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemInventories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPriceLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    CostPrice = table.Column<float>(nullable: false),
                    RetailPrice = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPriceLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemPriceLog_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    OrderNo = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    SalesOutletId = table.Column<Guid>(nullable: false),
                    StaffId = table.Column<Guid>(nullable: false),
                    VAT = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    OrderAmount = table.Column<float>(nullable: false),
                    TotalAmount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_SalesOutlets_SalesOutletId",
                        column: x => x.SalesOutletId,
                        principalTable: "SalesOutlets",
                        principalColumn: "SalesOutletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    OrderId1 = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    VAT = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    PaymentMethodID = table.Column<Guid>(nullable: true),
                    TransactionOrderId = table.Column<Guid>(nullable: true),
                    PaymentAmount = table.Column<float>(nullable: false),
                    OtherDetails = table.Column<string>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_TransactionOrderId",
                        column: x => x.TransactionOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "CreatedAt", "Name", "Terminus", "UpdatedAt" },
                values: new object[] { new Guid("5dcbd447-af87-457a-99f7-b62bb215c1fc"), new DateTime(2020, 12, 3, 23, 51, 16, 986, DateTimeKind.Utc).AddTicks(6485), "Unknown", "DESKTOP-V84PPA9", new DateTime(2020, 12, 3, 23, 51, 16, 986, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "AmountOfLastDeposit", "CreatedAt", "CurrentBalance", "DateOfBirth", "DateOfLastDeposit", "FullName", "Gender", "OtherDetails", "Terminus", "UpdatedAt" },
                values: new object[] { new Guid("f90879ce-9d68-48a6-aa00-0ef7e1efafaf"), 0f, new DateTime(2020, 12, 3, 23, 51, 16, 984, DateTimeKind.Utc).AddTicks(2330), 0f, new DateTime(1970, 12, 3, 23, 51, 16, 984, DateTimeKind.Utc).AddTicks(2273), new DateTime(2020, 12, 3, 23, 51, 16, 984, DateTimeKind.Utc).AddTicks(2345), "Walkin Customer", 2, "Anonymous customer", "DESKTOP-V84PPA9", new DateTime(2020, 12, 3, 23, 51, 16, 984, DateTimeKind.Utc).AddTicks(3235) });

            migrationBuilder.InsertData(
                table: "ItemCategory",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "Name", "Terminus", "UpdatedAt" },
                values: new object[] { new Guid("91da765d-6f8a-43cb-bdc6-be8b72567094"), new DateTime(2020, 12, 3, 23, 51, 16, 987, DateTimeKind.Utc).AddTicks(484), "Other", "Other", "DESKTOP-V84PPA9", new DateTime(2020, 12, 3, 23, 51, 16, 987, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.InsertData(
                table: "UnitOfMeasurements",
                columns: new[] { "ID", "CreatedAt", "Terminus", "UOMCode", "UOMDescription", "UpdatedAt" },
                values: new object[] { new Guid("16b71f08-da80-44f4-88c1-bf0f7d5aea93"), new DateTime(2020, 12, 3, 23, 51, 16, 987, DateTimeKind.Utc).AddTicks(6550), "DESKTOP-V84PPA9", "Each", "Each", new DateTime(2020, 12, 3, 23, 51, 16, 987, DateTimeKind.Utc).AddTicks(6555) });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustment_CostCenterId",
                table: "InventoryAdjustment",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustment_ItemId",
                table: "InventoryAdjustment",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventories_CostCenterId",
                table: "ItemInventories",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventories_ItemId",
                table: "ItemInventories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceLog_ItemId",
                table: "ItemPriceLog",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BrandId",
                table: "Items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UOMCodeId",
                table: "Items",
                column: "UOMCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId1",
                table: "OrderItems",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SalesOutletId",
                table: "Orders",
                column: "SalesOutletId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StaffId",
                table: "Orders",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodID",
                table: "Payments",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionOrderId",
                table: "Payments",
                column: "TransactionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOutlets_CompanyId",
                table: "SalesOutlets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_CostCenterId",
                table: "Staff",
                column: "CostCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryAdjustment");

            migrationBuilder.DropTable(
                name: "ItemInventories");

            migrationBuilder.DropTable(
                name: "ItemPriceLog");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "ItemCategory");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurements");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "SalesOutlets");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
