using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquillibriumERP.Sales.Migrations
{
    /// <inheritdoc />
    public partial class AddEstimateReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "Estimates",
                newName: "QuoteNumber");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Customers",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Estimates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Estimates",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Estimates",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedUtc",
                table: "Estimates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Estimates",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Estimates",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Estimates",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "Estimates",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Estimates",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "EstimateItems",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "EstimateItems",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "EstimateItems",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LineSubtotal",
                table: "EstimateItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LineTotal",
                table: "EstimateItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "EstimateItems",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "EstimateItems",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "EstimateItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasure",
                table: "EstimateItems",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Customers",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerCategoryId",
                table: "Customers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerType",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTerms",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TaxNumber",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Customers",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "CustomerContacts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerCategories",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CustomerCategories",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerCategories",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomerCategories",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "CustomerCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "CustomerAddresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EstimateSequences_Year",
                table: "EstimateSequences",
                column: "Year",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_QuoteNumber",
                table: "Estimates",
                column: "QuoteNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerCategoryId",
                table: "Customers",
                column: "CustomerCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId1",
                table: "CustomerContacts",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_Code",
                table: "CustomerCategories",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategories_Name",
                table: "CustomerCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId1",
                table: "CustomerAddresses",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId1",
                table: "CustomerAddresses",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContacts_Customers_CustomerId1",
                table: "CustomerContacts",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerCategories_CustomerCategoryId",
                table: "Customers",
                column: "CustomerCategoryId",
                principalTable: "CustomerCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId1",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContacts_Customers_CustomerId1",
                table: "CustomerContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerCategories_CustomerCategoryId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_EstimateSequences_Year",
                table: "EstimateSequences");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_QuoteNumber",
                table: "Estimates");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerCategoryId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContacts_CustomerId1",
                table: "CustomerContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCategories_Code",
                table: "CustomerCategories");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCategories_Name",
                table: "CustomerCategories");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_CustomerId1",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "ModifiedUtc",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "EstimateItems");

            migrationBuilder.DropColumn(
                name: "LineSubtotal",
                table: "EstimateItems");

            migrationBuilder.DropColumn(
                name: "LineTotal",
                table: "EstimateItems");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "EstimateItems");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "EstimateItems");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "EstimateItems");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "EstimateItems");

            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerCategoryId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TaxNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CustomerCategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerCategories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomerCategories");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "CustomerCategories");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "CustomerAddresses");

            migrationBuilder.RenameColumn(
                name: "QuoteNumber",
                table: "Estimates",
                newName: "ReferenceNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "CompanyName");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "EstimateItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "EstimateItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerCategories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);
        }
    }
}
