using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.MvcDemoApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTextElementsUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TextElements");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueTextId",
                table: "TextElements",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TextElements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TextElements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TextElements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TextElements_IsDeleted_UniqueTextId",
                table: "TextElements",
                columns: new[] { "IsDeleted", "UniqueTextId" },
                unique: true,
                filter: "IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TextElements_IsDeleted_UniqueTextId",
                table: "TextElements");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TextElements");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TextElements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TextElements");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueTextId",
                table: "TextElements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TextElements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
