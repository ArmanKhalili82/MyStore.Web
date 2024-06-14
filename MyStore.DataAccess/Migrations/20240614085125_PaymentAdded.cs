using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PaymentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CCV",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpiredDateMonth",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpiredDateYear",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCV",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ExpiredDateMonth",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ExpiredDateYear",
                table: "orders");
        }
    }
}
