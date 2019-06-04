using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class ItemQualityItemTypeConfigurationCreateDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ItemTypes",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ItemQualities",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ItemTypes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ItemQualities",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
