using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Rooms_RoomId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_AspNetUsers_UserId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_Violations_UserId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_RoomId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "ReadingDate",
                table: "UtilityUsages");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DateReported",
                table: "Violations",
                newName: "ViolationDate");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Utilities",
                newName: "UtilityName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "RoomTypes",
                newName: "BasePrice");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RoomTypes",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "RoomTypes",
                newName: "MaxOccupants");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Payments",
                newName: "TransactionCode");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payments",
                newName: "AmountPaid");

            migrationBuilder.RenameColumn(
                name: "MonthYear",
                table: "Invoices",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Contracts",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Deposit",
                table: "Contracts",
                newName: "DepositAmount");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Code");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EvidenceImage",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "UtilityUsages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "UtilityUsages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "UtilityUsages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "UsageQuantity",
                table: "UtilityUsages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "UtilityUsages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Utilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoomTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Method",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequesterId",
                table: "MaintenanceRequests",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResolvedAt",
                table: "MaintenanceRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MaintenanceRequests",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BillingMonth",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BillingYear",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceCode",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BedId",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContractCode",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Blocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalFloors",
                table: "Blocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssetCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beds_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surcharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurchargeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surcharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surcharges_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Violations_ContractId",
                table: "Violations",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilityUsages_InvoiceId",
                table: "UtilityUsages",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_RequesterId",
                table: "MaintenanceRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BedId",
                table: "Contracts",
                column: "BedId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_RoomId",
                table: "Assets",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Beds_RoomId",
                table: "Beds",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Surcharges_InvoiceId",
                table: "Surcharges",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Beds_BedId",
                table: "Contracts",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_RequesterId",
                table: "MaintenanceRequests",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilityUsages_Invoices_InvoiceId",
                table: "UtilityUsages",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_Contracts_ContractId",
                table: "Violations",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Beds_BedId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_RequesterId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilityUsages_Invoices_InvoiceId",
                table: "UtilityUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_Contracts_ContractId",
                table: "Violations");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "Surcharges");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Violations_ContractId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_UtilityUsages_InvoiceId",
                table: "UtilityUsages");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_RequesterId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_BedId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "EvidenceImage",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "UtilityUsages");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "UtilityUsages");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "UtilityUsages");

            migrationBuilder.DropColumn(
                name: "UsageQuantity",
                table: "UtilityUsages");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "UtilityUsages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Utilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "ResolvedAt",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "BillingMonth",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BillingYear",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BedId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractCode",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "TotalFloors",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdentityCardNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ViolationDate",
                table: "Violations",
                newName: "DateReported");

            migrationBuilder.RenameColumn(
                name: "UtilityName",
                table: "Utilities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "RoomTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "MaxOccupants",
                table: "RoomTypes",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "RoomTypes",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TransactionCode",
                table: "Payments",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "AmountPaid",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Invoices",
                newName: "MonthYear");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Contracts",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "DepositAmount",
                table: "Contracts",
                newName: "Deposit");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Violations",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadingDate",
                table: "UtilityUsages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "MaintenanceRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Violations_UserId",
                table: "Violations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_RoomId",
                table: "Contracts",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Rooms_RoomId",
                table: "Contracts",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_AspNetUsers_UserId",
                table: "Violations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
