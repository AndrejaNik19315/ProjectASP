using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class InventoryDependencyConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Characters_Inventories_InventoryId",
            //    table: "Characters");

            //migrationBuilder.DropIndex(
            //    name: "IX_Characters_InventoryId",
            //    table: "Characters");

            //migrationBuilder.DropColumn(
            //    name: "InventoryId",
            //    table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Inventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CharacterId",
                table: "Inventories",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Characters_CharacterId",
                table: "Inventories",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Characters_CharacterId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_CharacterId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Inventories");

            //migrationBuilder.AddColumn<int>(
            //    name: "InventoryId",
            //    table: "Characters",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Characters_InventoryId",
            //    table: "Characters",
            //    column: "InventoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Characters_Inventories_InventoryId",
            //    table: "Characters",
            //    column: "InventoryId",
            //    principalTable: "Inventories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
