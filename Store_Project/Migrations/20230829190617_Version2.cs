using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Asp.net_core.Migrations
{
    /// <inheritdoc />
    public partial class Version2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    CPF = table.Column<string>(type: "char(14)", nullable: false),
                    DateBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Address_Numero = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Complement = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Neighborhood = table.Column<string>(type: "TEXT", nullable: true),
                    Address_City = table.Column<string>(type: "TEXT", nullable: true),
                    Address_State = table.Column<string>(type: "TEXT", nullable: true),
                    Address_ZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Reference = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.IdUser);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
