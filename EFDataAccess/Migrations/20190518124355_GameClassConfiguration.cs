using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class GameClassConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GameClasses",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "GameClasses",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameClasses_Name",
                table: "GameClasses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GameClassId",
                table: "Characters",
                column: "GameClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_GameClasses_GameClassId",
                table: "Characters",
                column: "GameClassId",
                principalTable: "GameClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GameClasses_GameClassId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_GameClasses_Name",
                table: "GameClasses");

            migrationBuilder.DropIndex(
                name: "IX_Characters_GameClassId",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GameClasses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "GameClasses",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
