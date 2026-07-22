using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquillibriumERP.Manufacturing.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBomName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "BillOfMaterials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BillOfMaterials",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
