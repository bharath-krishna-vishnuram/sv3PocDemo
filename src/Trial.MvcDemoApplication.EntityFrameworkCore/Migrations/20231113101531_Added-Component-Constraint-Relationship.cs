using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.MvcDemoApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddedComponentConstraintRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentDescriptor_Components_AssociatedComponentId",
                table: "ComponentDescriptor");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssociatedComponentId",
                table: "ComponentDescriptor",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "ComponentComponentDescriptor",
                columns: table => new
                {
                    ConstraintComponentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConstraintDescriptorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentComponentDescriptor", x => new { x.ConstraintComponentsId, x.ConstraintDescriptorsId });
                    table.ForeignKey(
                        name: "FK_ComponentComponentDescriptor_ComponentDescriptor_ConstraintDescriptorsId",
                        column: x => x.ConstraintDescriptorsId,
                        principalTable: "ComponentDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentComponentDescriptor_Components_ConstraintComponentsId",
                        column: x => x.ConstraintComponentsId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentComponentDescriptor_ConstraintDescriptorsId",
                table: "ComponentComponentDescriptor",
                column: "ConstraintDescriptorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentDescriptor_Components_AssociatedComponentId",
                table: "ComponentDescriptor",
                column: "AssociatedComponentId",
                principalTable: "Components",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentDescriptor_Components_AssociatedComponentId",
                table: "ComponentDescriptor");

            migrationBuilder.DropTable(
                name: "ComponentComponentDescriptor");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssociatedComponentId",
                table: "ComponentDescriptor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentDescriptor_Components_AssociatedComponentId",
                table: "ComponentDescriptor",
                column: "AssociatedComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
