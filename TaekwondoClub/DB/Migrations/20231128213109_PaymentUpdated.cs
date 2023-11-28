using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class PaymentUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthYear",
                table: "Payments",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Payments",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Payments",
                newName: "MonthYear");
        }
    }
}
