using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editProductLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLocations",
                table: "ProductLocations");

            migrationBuilder.DropIndex(
                name: "IX_ProductLocations_ProductId",
                table: "ProductLocations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductLocations");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductLocations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductLocations");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ProductLocations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ProductLocations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ProductLocations");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProductLocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLocations",
                table: "ProductLocations",
                columns: new[] { "ProductId", "GovernorateId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLocations",
                table: "ProductLocations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductLocations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductLocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "ProductLocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ProductLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ProductLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProductLocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLocations",
                table: "ProductLocations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocations_ProductId",
                table: "ProductLocations",
                column: "ProductId");
        }
    }
}
