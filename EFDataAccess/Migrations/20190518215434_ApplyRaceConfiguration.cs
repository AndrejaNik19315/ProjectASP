using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class ApplyRaceConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Races",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Races",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Races_Name",
                table: "Races",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Races_Name",
                table: "Races");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Races",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Races",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Races_RaceId",
                table: "Characters",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
