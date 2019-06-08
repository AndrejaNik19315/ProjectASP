using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class ChangedItemIsForSaleToInStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isForSale",
                table: "Items",
                newName: "inStock");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Items",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "inStock",
                table: "Items",
                newName: "isForSale");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Items",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldNullable: true,
                oldDefaultValue: 0);
        }
    }
}
