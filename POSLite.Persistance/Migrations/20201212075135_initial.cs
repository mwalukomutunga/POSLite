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
                    Address = table.Column<string>(nullable: true),
                    CurrentBalance = table.Column<decimal>(nullable: false),
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
                    CostCenterId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<long>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
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
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    InvoiceNo = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    SalesOutletId = table.Column<Guid>(nullable: false),
                    StaffId = table.Column<Guid>(nullable: false),
                    VAT = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    InvoiceAmount = table.Column<float>(nullable: false),
                    TotalAmount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_SalesOutlets_SalesOutletId",
                        column: x => x.SalesOutletId,
                        principalTable: "SalesOutlets",
                        principalColumn: "SalesOutletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
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
                name: "InvoiceLineItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Qty = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    Total = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Terminus = table.Column<string>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    Qty = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    VAT = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
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
                    CustomerId = table.Column<Guid>(nullable: false),
                    OtherDetails = table.Column<string>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[] { new Guid("8a4b2223-2100-4200-b3a2-b7e7caec025c"), new DateTime(2020, 12, 12, 10, 51, 34, 774, DateTimeKind.Utc).AddTicks(5958), "Unknown", "DESKTOP-V84PPA9", new DateTime(2020, 12, 12, 10, 51, 34, 774, DateTimeKind.Utc).AddTicks(5964) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "AmountOfLastDeposit", "CreatedAt", "CurrentBalance", "DateOfBirth", "DateOfLastDeposit", "FullName", "Gender", "OtherDetails", "Terminus", "UpdatedAt" },
                values: new object[] { new Guid("dc2b26e5-828d-4aac-b4b2-1ed3adec151e"), null, 0f, new DateTime(2020, 12, 12, 10, 51, 34, 773, DateTimeKind.Utc).AddTicks(6356), 0m, new DateTime(1970, 12, 12, 10, 51, 34, 773, DateTimeKind.Utc).AddTicks(6335), new DateTime(2020, 12, 12, 10, 51, 34, 773, DateTimeKind.Utc).AddTicks(6361), "Walkin Customer", 2, "Anonymous customer", "DESKTOP-V84PPA9", new DateTime(2020, 12, 12, 10, 51, 34, 773, DateTimeKind.Utc).AddTicks(7009) });

            migrationBuilder.InsertData(
                table: "ItemCategory",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "Name", "Terminus", "UpdatedAt" },
                values: new object[] { new Guid("120a6d1f-2ee0-4599-a575-34af95767cbd"), new DateTime(2020, 12, 12, 10, 51, 34, 774, DateTimeKind.Utc).AddTicks(7490), "Other", "Other", "DESKTOP-V84PPA9", new DateTime(2020, 12, 12, 10, 51, 34, 774, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.InsertData(
                table: "UnitOfMeasurements",
                columns: new[] { "ID", "CreatedAt", "Terminus", "UOMCode", "UOMDescription", "UpdatedAt" },
                values: new object[] { new Guid("1af2581b-6423-4408-a953-7fa943306c57"), new DateTime(2020, 12, 12, 10, 51, 34, 774, DateTimeKind.Utc).AddTicks(9860), "DESKTOP-V84PPA9", "Each", "Each", new DateTime(2020, 12, 12, 10, 51, 34, 774, DateTimeKind.Utc).AddTicks(9860) });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustment_CostCenterId",
                table: "InventoryAdjustment",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustment_ItemId",
                table: "InventoryAdjustment",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_InvoiceId",
                table: "InvoiceLineItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_ItemId",
                table: "InvoiceLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesOutletId",
                table: "Invoices",
                column: "SalesOutletId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StaffId",
                table: "Invoices",
                column: "StaffId");

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
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

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
                name: "IX_Payments_CustomerId",
                table: "Payments",
                column: "CustomerId");

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
                name: "InvoiceLineItems");

            migrationBuilder.DropTable(
                name: "ItemInventories");

            migrationBuilder.DropTable(
                name: "ItemPriceLog");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Invoices");

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
