using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class InventoryConfigurationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxSlots",
                table: "Inventories",
                maxLength: 3,
                nullable: true,
                defaultValue: 20,
                oldClrType: typeof(int),
                oldMaxLength: 3,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxSlots",
                table: "Inventories",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 3,
                oldNullable: true,
                oldDefaultValue: 20);
        }
    }
}
