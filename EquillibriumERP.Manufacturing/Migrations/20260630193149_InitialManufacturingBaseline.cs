using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquillibriumERP.Manufacturing.Migrations
{
    /// <inheritdoc />
    public partial class InitialManufacturingBaseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillOfMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillOfMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BOMStepMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BOMStepId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    RawMaterialProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOMStepMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StepMaterialConsumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderStepId = table.Column<Guid>(type: "uuid", nullable: false),
                    RawMaterialProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityUsed = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepMaterialConsumptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderStepId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecutedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Workstation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WorkOrderMaterialId = table.Column<Guid>(type: "uuid", nullable: true),
                    RawMaterialLotNo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ExpectedQuantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    ActualQuantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillOfMaterialItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillOfMaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    RawMaterialProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillOfMaterialItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillOfMaterialItems_BillOfMaterials_BillOfMaterialId",
                        column: x => x.BillOfMaterialId,
                        principalTable: "BillOfMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BOMSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillOfMaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    RawMaterialProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    QuantityPercentage = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOMSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BOMSteps_BillOfMaterials_BillOfMaterialId",
                        column: x => x.BillOfMaterialId,
                        principalTable: "BillOfMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillOfMaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlannedQuantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BatchNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LotNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_BillOfMaterials_BillOfMaterialId",
                        column: x => x.BillOfMaterialId,
                        principalTable: "BillOfMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductBatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    BatchNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityProduced = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBatches_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RawMaterialProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpectedQuantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IssuedQuantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    ConsumedQuantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderMaterials_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    BOMProcessStepId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderMaterialId = table.Column<Guid>(type: "uuid", nullable: true),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderSteps_WorkOrderMaterials_WorkOrderMaterialId",
                        column: x => x.WorkOrderMaterialId,
                        principalTable: "WorkOrderMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderSteps_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialConsumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderStepId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RawMaterialProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    LotNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialConsumptions_WorkOrderSteps_WorkOrderStepId",
                        column: x => x.WorkOrderStepId,
                        principalTable: "WorkOrderSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialConsumptions_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterialItems_BillOfMaterialId",
                table: "BillOfMaterialItems",
                column: "BillOfMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterialItems_RawMaterialProductId",
                table: "BillOfMaterialItems",
                column: "RawMaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterials_Code",
                table: "BillOfMaterials",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterials_ProductId",
                table: "BillOfMaterials",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BOMStepMaterials_BOMStepId",
                table: "BOMStepMaterials",
                column: "BOMStepId");

            migrationBuilder.CreateIndex(
                name: "IX_BOMStepMaterials_RawMaterialProductId",
                table: "BOMStepMaterials",
                column: "RawMaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BOMSteps_BillOfMaterialId",
                table: "BOMSteps",
                column: "BillOfMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_BOMSteps_BillOfMaterialId_StepNumber",
                table: "BOMSteps",
                columns: new[] { "BillOfMaterialId", "StepNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_LotNumber",
                table: "MaterialConsumptions",
                column: "LotNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_RawMaterialProductId",
                table: "MaterialConsumptions",
                column: "RawMaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_WorkOrderId",
                table: "MaterialConsumptions",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_WorkOrderStepId",
                table: "MaterialConsumptions",
                column: "WorkOrderStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBatches_BatchNumber",
                table: "ProductBatches",
                column: "BatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBatches_WorkOrderId",
                table: "ProductBatches",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StepMaterialConsumptions_RawMaterialProductId",
                table: "StepMaterialConsumptions",
                column: "RawMaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StepMaterialConsumptions_WorkOrderStepId",
                table: "StepMaterialConsumptions",
                column: "WorkOrderStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderMaterials_RawMaterialProductId",
                table: "WorkOrderMaterials",
                column: "RawMaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderMaterials_WorkOrderId",
                table: "WorkOrderMaterials",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_BatchNo",
                table: "WorkOrders",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_BillOfMaterialId",
                table: "WorkOrders",
                column: "BillOfMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_LotNo",
                table: "WorkOrders",
                column: "LotNo");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_Status",
                table: "WorkOrders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderSteps_BOMProcessStepId",
                table: "WorkOrderSteps",
                column: "BOMProcessStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderSteps_WorkOrderId",
                table: "WorkOrderSteps",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderSteps_WorkOrderId_StepNumber",
                table: "WorkOrderSteps",
                columns: new[] { "WorkOrderId", "StepNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderSteps_WorkOrderMaterialId",
                table: "WorkOrderSteps",
                column: "WorkOrderMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTransactions_ExecutedAt",
                table: "WorkOrderTransactions",
                column: "ExecutedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTransactions_WorkOrderId",
                table: "WorkOrderTransactions",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTransactions_WorkOrderMaterialId",
                table: "WorkOrderTransactions",
                column: "WorkOrderMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTransactions_WorkOrderStepId",
                table: "WorkOrderTransactions",
                column: "WorkOrderStepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillOfMaterialItems");

            migrationBuilder.DropTable(
                name: "BOMStepMaterials");

            migrationBuilder.DropTable(
                name: "BOMSteps");

            migrationBuilder.DropTable(
                name: "MaterialConsumptions");

            migrationBuilder.DropTable(
                name: "ProductBatches");

            migrationBuilder.DropTable(
                name: "StepMaterialConsumptions");

            migrationBuilder.DropTable(
                name: "WorkOrderTransactions");

            migrationBuilder.DropTable(
                name: "WorkOrderSteps");

            migrationBuilder.DropTable(
                name: "WorkOrderMaterials");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "BillOfMaterials");
        }
    }
}
