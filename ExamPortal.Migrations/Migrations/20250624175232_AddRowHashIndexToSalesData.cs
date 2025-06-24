using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamPortal.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddRowHashIndexToSalesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesData_OrderId",
                table: "SalesData");

            migrationBuilder.AddColumn<string>(
                name: "RowHash",
                table: "SalesData",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesData_RowHash",
                table: "SalesData",
                column: "RowHash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesData_RowHash",
                table: "SalesData");

            migrationBuilder.DropColumn(
                name: "RowHash",
                table: "SalesData");

            migrationBuilder.CreateIndex(
                name: "IX_SalesData_OrderId",
                table: "SalesData",
                column: "OrderId");
        }
    }
}
