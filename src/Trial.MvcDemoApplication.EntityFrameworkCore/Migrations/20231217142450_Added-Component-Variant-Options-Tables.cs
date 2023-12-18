using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.MvcDemoApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedComponentVariantOptionsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentVariantSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VariantSetComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentVariantSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentVariantSet_Components_VariantSetComponentId",
                        column: x => x.VariantSetComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariantOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    AssociatedVariantSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociatedDescriptorOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantOption_ComponentVariantSet_AssociatedVariantSetId",
                        column: x => x.AssociatedVariantSetId,
                        principalTable: "ComponentVariantSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantOption_Options_AssociatedDescriptorOptionId",
                        column: x => x.AssociatedDescriptorOptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentVariantSet_VariantSetComponentId",
                table: "ComponentVariantSet",
                column: "VariantSetComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantOption_AssociatedDescriptorOptionId",
                table: "VariantOption",
                column: "AssociatedDescriptorOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantOption_AssociatedVariantSetId",
                table: "VariantOption",
                column: "AssociatedVariantSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantOption");

            migrationBuilder.DropTable(
                name: "ComponentVariantSet");
        }
    }
}
