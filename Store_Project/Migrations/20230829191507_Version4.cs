using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Asp.net_core.Migrations
{
    /// <inheritdoc />
    public partial class Version4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_CustomerModelIdUser",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Address",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "CustomerModelIdUser",
                table: "Address",
                newName: "IdAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Address_IdUser",
                table: "Address",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_IdUser",
                table: "Address",
                column: "IdUser",
                principalTable: "Customer",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_IdUser",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_IdUser",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Address",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdAddress",
                table: "Address",
                newName: "CustomerModelIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_CustomerModelIdUser",
                table: "Address",
                column: "CustomerModelIdUser",
                principalTable: "Customer",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
