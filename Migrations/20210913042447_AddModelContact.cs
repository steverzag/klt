using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloraYFaunaAPI.Migrations
{
    public partial class AddModelContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkAsRead = table.Column<bool>(type: "bit", nullable: false),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "dbo");
        }
    }
}
