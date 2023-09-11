using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Asp.net_core.Migrations
{
    /// <inheritdoc />
    public partial class Version7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "User",
                type: "TEXT",
                nullable: true,
                defaultValueSql: "datetime('now', 'localtime', 'start of day')",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true,
                oldDefaultValueSql: "datatime('now', 'localtime', 'start of day')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "User",
                type: "TEXT",
                nullable: true,
                defaultValueSql: "datatime('now', 'localtime', 'start of day')",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true,
                oldDefaultValueSql: "datetime('now', 'localtime', 'start of day')");
        }
    }
}
