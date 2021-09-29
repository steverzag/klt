using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloraYFaunaAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Carousel",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carousel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    Author = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newsletters",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Metadata_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Metadata_CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Metadata_IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IpPosts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpPosts_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalSchema: "dbo",
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CategoryId",
                schema: "dbo",
                table: "BlogPosts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "dbo",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IpPosts_BlogPostId",
                schema: "dbo",
                table: "IpPosts",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Newsletters_Email",
                schema: "dbo",
                table: "Newsletters",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "dbo",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carousel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Gallery",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IpPosts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Newsletters",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BlogPosts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "dbo");
        }
    }
}
