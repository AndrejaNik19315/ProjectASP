using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class ItemQualityConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemTypes",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AddColumn<int>(
                name: "ItemQualityId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemQualities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemQualities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemQualityId",
                table: "Items",
                column: "ItemQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemQualities_Name",
                table: "ItemQualities",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemQualities_ItemQualityId",
                table: "Items",
                column: "ItemQualityId",
                principalTable: "ItemQualities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemQualities_ItemQualityId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemQualities");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemQualityId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemQualityId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemTypes",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 24);
        }
    }
}
