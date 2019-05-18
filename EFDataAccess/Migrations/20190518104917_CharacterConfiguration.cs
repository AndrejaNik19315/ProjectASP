using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class CharacterConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Users",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "Users",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Characters",
                maxLength: 80,
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Funds",
                table: "Characters",
                maxLength: 1000,
                nullable: true,
                defaultValue: 5.0m,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Characters",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                table: "Characters",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_Name",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Users",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "Users",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 80,
                oldNullable: true,
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Funds",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(decimal),
                oldMaxLength: 1000,
                oldNullable: true,
                oldDefaultValue: 5.0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
