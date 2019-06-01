using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class InventoryItemConfigurationRemovedBoughtOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoughtOn",
                table: "InventoriyItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "InventoriyItems",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "InventoriyItems",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "BoughtOn",
                table: "InventoriyItems",
                nullable: true,
                defaultValueSql: "GETDATE()");
        }
    }
}
