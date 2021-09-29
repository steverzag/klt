using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloraYFaunaAPI.Migrations
{
    public partial class UpdateModelRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "dbo",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Expires",
                schema: "dbo",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReasonRevoked",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplacedByToken",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Revoked",
                schema: "dbo",
                table: "RefreshToken",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevokedByIp",
                schema: "dbo",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Expires",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "ReasonRevoked",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "ReplacedByToken",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Revoked",
                schema: "dbo",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "RevokedByIp",
                schema: "dbo",
                table: "RefreshToken");
        }
    }
}
