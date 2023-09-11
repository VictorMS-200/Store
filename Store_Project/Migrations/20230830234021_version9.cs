using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Asp.net_core.Migrations
{
    /// <inheritdoc />
    public partial class version9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_Street",
                table: "Order",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Address",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Street",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Address");
        }
    }
}
