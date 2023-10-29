using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.MvcDemoApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddStructureTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueTextId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProduct = table.Column<bool>(type: "bit", nullable: false),
                    IsSubProduct = table.Column<bool>(type: "bit", nullable: false),
                    IsStructure = table.Column<bool>(type: "bit", nullable: false),
                    IsComponent = table.Column<bool>(type: "bit", nullable: false),
                    IsDescriptor = table.Column<bool>(type: "bit", nullable: false),
                    IsOption = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    German = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    French = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spanish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Russian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chinese = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Components_ParentComponentId",
                        column: x => x.ParentComponentId,
                        principalTable: "Components",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Components_TextElements_NameId",
                        column: x => x.NameId,
                        principalTable: "TextElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComponentDescriptor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssociatedComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ComponentDescriptor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentDescriptor_Components_AssociatedComponentId",
                        column: x => x.AssociatedComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentDescriptor_TextElements_NameId",
                        column: x => x.NameId,
                        principalTable: "TextElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Structures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RootComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Structures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Structures_Components_RootComponentId",
                        column: x => x.RootComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Structures_TextElements_NameId",
                        column: x => x.NameId,
                        principalTable: "TextElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociatedDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_ComponentDescriptor_AssociatedDescriptorId",
                        column: x => x.AssociatedDescriptorId,
                        principalTable: "ComponentDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Options_TextElements_NameId",
                        column: x => x.NameId,
                        principalTable: "TextElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StructureElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociatedStructureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElementOrder = table.Column<int>(type: "int", nullable: false),
                    DescriptorOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureElements_Components_SelectedComponentId",
                        column: x => x.SelectedComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructureElements_Options_DescriptorOptionId",
                        column: x => x.DescriptorOptionId,
                        principalTable: "Options",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StructureElements_Structures_AssociatedStructureId",
                        column: x => x.AssociatedStructureId,
                        principalTable: "Structures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDescriptor_AssociatedComponentId",
                table: "ComponentDescriptor",
                column: "AssociatedComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDescriptor_NameId",
                table: "ComponentDescriptor",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_NameId",
                table: "Components",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ParentComponentId",
                table: "Components",
                column: "ParentComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_AssociatedDescriptorId",
                table: "Options",
                column: "AssociatedDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_NameId",
                table: "Options",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureElements_AssociatedStructureId",
                table: "StructureElements",
                column: "AssociatedStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureElements_DescriptorOptionId",
                table: "StructureElements",
                column: "DescriptorOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureElements_SelectedComponentId",
                table: "StructureElements",
                column: "SelectedComponentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Structures_NameId",
                table: "Structures",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_RootComponentId",
                table: "Structures",
                column: "RootComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StructureElements");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Structures");

            migrationBuilder.DropTable(
                name: "ComponentDescriptor");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "TextElements");
        }
    }
}
