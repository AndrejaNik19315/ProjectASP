using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class GenderConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Genders",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Genders",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Sex",
                table: "Genders",
                column: "Sex",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GenderId",
                table: "Characters",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Genders_GenderId",
                table: "Characters",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Genders_GenderId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Genders_Sex",
                table: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Characters_GenderId",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Genders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Genders",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
