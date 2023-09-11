using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Asp.net_core.Migrations
{
    /// <inheritdoc />
    public partial class Version3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_Complement",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_Neighborhood",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_Numero",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_Reference",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Customer");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    CustomerModelIdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Numero = table.Column<string>(type: "TEXT", nullable: true),
                    Complement = table.Column<string>(type: "TEXT", nullable: true),
                    Neighborhood = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    Reference = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => new { x.CustomerModelIdUser, x.Id });
                    table.ForeignKey(
                        name: "FK_Address_Customer_CustomerModelIdUser",
                        column: x => x.CustomerModelIdUser,
                        principalTable: "Customer",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Complement",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Neighborhood",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Numero",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Reference",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Customer",
                type: "TEXT",
                nullable: true);
        }
    }
}
